using Microsoft.Extensions.DependencyInjection;
using Sdk.Api.Interfaces;
using Sdk.Api.StateMachines;

namespace Billings.Extensions
{
    public static class ConfigureStateMachine
    {
        public static IServiceCollection AddStateMachine(this IServiceCollection services)
        {
            services.AddTransient<ITransactionsContext, TransactionsContext>();
            return services;
        }
    }
}