using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.EFCore.Extensions {
    public static class ServicesExtensions {

        public static IServiceCollection AddUnitOfWork<TDbContext> (this IServiceCollection services, Action<UnitOfWorkConfig>? configure = null)
            where TDbContext : DbContext {

            if (configure != null) {
                services.Configure(configure);
            }

            services.TryAddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();

            return services;
        }

    }
}
