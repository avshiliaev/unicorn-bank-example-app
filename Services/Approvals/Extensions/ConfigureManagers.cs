using Approvals.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;

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