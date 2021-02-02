using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;
using Transactions.Interfaces;
using Transactions.Managers;

namespace Transactions.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services
                .AddTransient<IEventStoreManager<ITransactionModel>, EventStoreManager>()
                .AddTransient<IConcurrencyManager, ConcurrencyManager>();
            return services;
        }
    }
}