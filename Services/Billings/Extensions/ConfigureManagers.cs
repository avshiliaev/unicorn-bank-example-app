using Billings.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;

namespace Billings.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services
                .AddTransient<IEventStoreManager<ATransactionsState>, EventStoreManager>();
            return services;
        }
    }
}