using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sdk.Persistence.Interfaces;
using Sdk.Persistence.Models;

namespace Sdk.Persistence.Extensions
{
    public static class ConfigureMongoDb
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettingsModel>(
                configuration.GetSection("MongoSettings")
            );
            services.AddSingleton<IMongoSettingsModel>(sp =>
                sp.GetRequiredService<IOptions<MongoSettingsModel>>().Value);

            return services;
        }
    }
}