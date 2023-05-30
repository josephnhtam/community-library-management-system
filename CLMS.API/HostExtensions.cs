using Microsoft.EntityFrameworkCore;
using Domain.Events.Extensions;
using Infrastructure.EFCore;
using Infrastructure.EFCore.DomainEvents.Extensions;
using Infrastructure.EFCore.Extensions;
using CLMS.Infrastructure;
using CLMS.Domain.Aggregates.AuthorAggregate;
using CLMS.Infrastructure.Repositories;
using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate;
using MediatR;
using System.Reflection;
using CLMS.Application;
using CLMS.Application.PipelineBehaviors;

namespace CLMS.API {
    public static class HostExtensions {

        public static WebApplicationBuilder ConfigureServices (this WebApplicationBuilder builder) {
            builder.AddMediatR()
                   .AddDbContext()
                   .AddRepositories()
                   .AddDomainEvents()
                   .AddUnitOfWork();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            return builder;
        }

        private static WebApplicationBuilder AddMediatR (this WebApplicationBuilder builder) {
            builder.Services.AddMediatR(typeof(App).Assembly);

            builder.Services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(OptimisticUpdatePipelineBehavior<,>));

            return builder;
        }

        private static WebApplicationBuilder AddDbContext (this WebApplicationBuilder builder) {
            var configuration = builder.Configuration;

            string migrationAssembly = Assembly.GetAssembly(typeof(HostExtensions))!.GetName().Name!;

            var connectionString = configuration.GetConnectionString("PostgreSQL")!;

            builder.Services.AddDbContext<LibraryDbContext>(options => {
                options.UseNpgsql(connectionString, options => {
                    options.MigrationsAssembly(migrationAssembly);
                    options.EnableRetryOnFailure();
                });
            });

            return builder;
        }

        private static WebApplicationBuilder AddRepositories (this WebApplicationBuilder builder) {
            builder.Services
                .AddScoped<IAuthorRepository, AuthorRepository>()
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IPatronRepository, PatronRepository>();

            return builder;
        }

        private static WebApplicationBuilder AddDomainEvents (this WebApplicationBuilder builder) {
            builder.Services.AddDomainEventDispatcher();
            builder.Services.AddDomainEventAccessor<LibraryDbContext>();

            return builder;
        }

        private static WebApplicationBuilder AddUnitOfWork (this WebApplicationBuilder builder) {
            builder.Services.AddUnitOfWork<LibraryDbContext>();

            return builder;
        }

        public static WebApplication ConfigurePipeline (this WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            } else {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }

        public static async Task InitializeAsync (this WebApplication app) {
            var config = app.Configuration.GetSection("Initialization");

            if (config.GetValue<bool>("MigrateDatabase")) {
                await app.MigrateDatabase<LibraryDbContext>();
            }
        }

    }
}
