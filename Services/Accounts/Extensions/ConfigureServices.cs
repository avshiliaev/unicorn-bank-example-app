using Accounts.Services;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreService<AAccountState>, EventStoreService>();
            return services;
        }
    }
}