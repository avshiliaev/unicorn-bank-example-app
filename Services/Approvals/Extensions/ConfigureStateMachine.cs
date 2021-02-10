using Microsoft.Extensions.DependencyInjection;
using Sdk.StateMachine.Interfaces;
using Sdk.StateMachine.StateMachines;

namespace Approvals.Extensions
{
    public static class ConfigureStateMachine
    {
        public static IServiceCollection AddStateMachine(this IServiceCollection services)
        {
            services.AddTransient<IAccountContext, AccountContext>();
            services.AddTransient<ITransactionsContext, TransactionsContext>();
            return services;
        }
    }
}