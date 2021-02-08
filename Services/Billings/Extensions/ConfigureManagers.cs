using Billings.Managers;
using Microsoft.Extensions.DependencyInjection;

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