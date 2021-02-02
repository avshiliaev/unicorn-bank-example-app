using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Interfaces;
using Sdk.Api.StateMachines;

namespace Approvals.Extensions
{
    public static class ConfigureStateMachine
    {
        public static IServiceCollection AddStateMachine(this IServiceCollection services)
        {
            services.AddTransient<IAccountContext, AccountContext>();
            return services;
        }
    }
}