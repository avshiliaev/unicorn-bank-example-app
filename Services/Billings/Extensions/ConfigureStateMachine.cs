using Microsoft.Extensions.DependencyInjection;

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