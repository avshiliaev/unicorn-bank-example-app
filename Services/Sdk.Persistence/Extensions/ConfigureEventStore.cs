using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Extensions
{
    public static class ConfigureEventStore
    {
        public static IServiceCollection AddEventStore<TRepository, TEntity, TContext>(
            this IServiceCollection services,
            IConfiguration configuration
        )
            where TContext : DbContext
            where TEntity : class, IEventRecord
            where TRepository : class, IRepository<TEntity>
        {
            services.AddDbContext<TContext>(
                options =>
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSql"))
            );
            services.AddTransient<IRepository<TEntity>, TRepository>();
            return services;
        }
    }
}