using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;
using Sdk.Persistence.Models;

namespace Sdk.Persistence.Extensions
{
    public static class ConfigureMongoDb
    {
        public static IServiceCollection AddMongoDb<TRepository, TEntity>(
            this IServiceCollection services, IConfiguration configuration
        )
            where TRepository : AbstractMongoRepository<TEntity>
            where TEntity : class, IMongoEntity
        {
            services.Configure<MongoSettingsModel>(
                configuration.GetSection("MongoSettings")
            );
            services.AddSingleton<IMongoSettingsModel>(sp =>
                sp.GetRequiredService<IOptions<MongoSettingsModel>>().Value);

            services.AddTransient<AbstractMongoRepository<TEntity>, TRepository>();
            return services;
        }
    }
}