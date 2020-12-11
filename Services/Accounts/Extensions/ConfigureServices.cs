using Accounts.Interfaces;
using Accounts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountsService, AccountsService>();
            return services;
        }
    }
}