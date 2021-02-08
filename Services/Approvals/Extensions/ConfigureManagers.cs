using Approvals.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Approvals.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreManager<AAccountState>, EventStoreManager>();
            return services;
        }
    }
}