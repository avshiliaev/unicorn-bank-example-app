using Accounts.Handlers;
using Accounts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Extensions
{
    public static class ConfigureHandlers
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAccountApprovedHandler, AccountApprovedHandler>();
            services.AddTransient<ITransactionPlacedHandler, TransactionPlacedHandler>();

            return services;
        }
    }
}