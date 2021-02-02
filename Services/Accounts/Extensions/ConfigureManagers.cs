using Accounts.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreManager<IAccountModel>, EventStoreManager>();
            return services;
        }
    }
}