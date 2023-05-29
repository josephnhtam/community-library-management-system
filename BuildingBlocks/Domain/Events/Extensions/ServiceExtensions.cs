using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Domain.Events.Extensions {
    public static class ServiceExtensions {
        public static IServiceCollection AddDomainEventDispatcher (this IServiceCollection services) {
            services.TryAddScoped<IDomainEventsDispatcher, DomainEventDispatcher>();
            return services;
        }
    }
}
