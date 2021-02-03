using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Interfaces;
using Sdk.Api.StateMachines;

namespace Transactions.Extensions
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