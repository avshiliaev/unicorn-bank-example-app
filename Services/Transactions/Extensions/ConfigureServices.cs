using Microsoft.Extensions.DependencyInjection;
using Transactions.Interfaces;
using Transactions.Services;

namespace Transactions.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITransactionsService, TransactionsService>()
                .AddTransient<IAccountsService, AccountsService>();
            return services;
        }
    }
}