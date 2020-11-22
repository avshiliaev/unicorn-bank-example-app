using Accounts.Interfaces;
using Accounts.Managers;
using Accounts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IAccountsManager, AccountsManager>();

            return services;
        }
    }
}