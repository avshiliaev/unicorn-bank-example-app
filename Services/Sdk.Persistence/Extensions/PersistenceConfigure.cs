using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Extensions
{
    public static class PersistenceConfigure
    {
        public static IServiceCollection AddCustomDatabase<TRepository, TEntity, TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString
        )
            where TContext : DbContext
            where TEntity : class, IEntity
            where TRepository : class, IRepository<TEntity>

        {
            services.AddDbContext<TContext>(
                options =>
                    options.UseNpgsql(configuration.GetConnectionString(connectionString))
            );

            services.AddTransient<IRepository<TEntity>, TRepository>();

            return services;
        }
    }
}