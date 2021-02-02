using Approvals.Abstractions;
using Approvals.Interfaces;
using Approvals.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Interfaces;

namespace Approvals.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services
                .AddTransient<IEventStoreManager<AbstractAccountState>, EventStoreManager>();
            return services;
        }
    }
}