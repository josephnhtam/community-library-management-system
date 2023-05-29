using Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.DomainEvents {
    public class DomainEventAccessor<TDbContext> : IDomainEventsAccessor
        where TDbContext : DbContext {

        private readonly TDbContext _dbContext;

        public DomainEventAccessor (TDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents () {
            return GetDomainEventEmitters()
                .SelectMany(e => e.DomainEvents)
                .ToList();
        }

        public void ClearDomainEvents () {
            foreach (var domainEventEmitter in GetDomainEventEmitters()) {
                domainEventEmitter.RemoveAllDomainEvents();
            }
        }

        private IEnumerable<IDomainEventEmitter> GetDomainEventEmitters () {
            return _dbContext.ChangeTracker.Entries()
                .Select(x => x.Entity)
                .OfType<IDomainEventEmitter>();
        }

    }
}
