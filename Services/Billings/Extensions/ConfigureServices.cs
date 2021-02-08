using Billings.Persistence.Entities;
using Billings.Services;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;

namespace Billings.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreService<TransactionEntity>, EventStoreService>();
            return services;
        }
    }
}