using Application.Exceptions;
using Domain.Contracts;
using Domain.Events;
using Infrastructure.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace Infrastructure.EFCore {
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext {

        private readonly TDbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventDispatcher;
        private readonly UnitOfWorkConfig _config;
        private readonly ILogger<UnitOfWork<TDbContext>> _logger;

        public UnitOfWork (TDbContext dbContext, IDomainEventsDispatcher domainEventDispatcher, IOptions<UnitOfWorkConfig> config, ILogger<UnitOfWork<TDbContext>> logger) {
            _dbContext = dbContext;
            _domainEventDispatcher = domainEventDispatcher;
            _config = config.Value;
            _logger = logger;
        }

        public async Task CommitAsync (CancellationToken cancellationToken = default) {
            await _domainEventDispatcher.DispatchDomainEventsAsync();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task ExecuteOptimisticTransactionAsync (Func<Task> task, Action<ITransactionOptions>? configureOptions = null, CancellationToken cancellationToken = default) {
            await ExecuteOptimisticTransactionAsync(
                async () => await ExecuteTransactionAsync(task),
                configureOptions,
                cancellationToken);
        }

        public async Task ExecuteOptimisticUpdateAsync (Func<Task> task) {
            int retries = _config.OptimisticConcurrencyConflictRetryCount;
            var retryPolicy =
                Policy.Handle<DbUpdateConcurrencyException>()
                    .WaitAndRetryAsync(retries,
                        (attempt) => TimeSpan.FromMilliseconds(10 * Math.Pow(2, attempt)),
                        (ex, timespan, context) => {
                            _logger.LogWarning(ex,
                                $"A conflict occured during the optimistic update. Retrying the update after {timespan.Milliseconds}ms.");
                            ResetContext();
                        });

            try {
                await retryPolicy.ExecuteAsync(task);
            } catch (DbUpdateConcurrencyException ex) {
                _logger.LogError(ex, "A conflict occured during the optimistic update. Exceeded max retry count.");
                throw new TransientException("Optimistic concurrency conflict", ex);
            }
        }

        public async Task ExecuteTransactionAsync (Func<Task> task, Action<ITransactionOptions>? configureOptions = null, CancellationToken cancellationToken = default) {
            if (IsInTransaction()) {
                await task.Invoke();
                return;
            }

            var options = new EfCoreTransactionOptions();

            if (configureOptions != null) {
                configureOptions.Invoke(options);
            }

            await _dbContext.ExecuteResilientTransaction(task, options.IsolationLevel, options.ResetContext, cancellationToken);
        }

        public bool IsInTransaction () {
            return _dbContext.Database.CurrentTransaction != null;
        }

        public void ResetContext () {
            _dbContext.ChangeTracker.Clear();
        }

    }
}
