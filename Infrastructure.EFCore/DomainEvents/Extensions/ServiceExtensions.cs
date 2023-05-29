using Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.EFCore.DomainEvents.Extensions {
    public static class ServiceExtensions {

        public static IServiceCollection AddDomainEventAccessor<TDbContext> (this IServiceCollection services)
            where TDbContext : DbContext {
            services.TryAddScoped<IDomainEventsAccessor, DomainEventAccessor<TDbContext>>();
            return services;
        }

    }
}
