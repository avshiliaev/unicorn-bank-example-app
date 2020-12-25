using Microsoft.Extensions.DependencyInjection;
using Transactions.Interfaces;
using Transactions.Managers;

namespace Transactions.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<ITransactionsManager, TransactionsManager>();
            return services;
        }
    }
}