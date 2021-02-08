using Accounts.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;

namespace Accounts.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IStatesManager, StatesManager>();
            return services;
        }
    }
}