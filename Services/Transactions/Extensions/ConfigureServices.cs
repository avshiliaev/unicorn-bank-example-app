using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;
using Transactions.Persistence.Entities;
using Transactions.Services;

namespace Transactions.Extensions
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