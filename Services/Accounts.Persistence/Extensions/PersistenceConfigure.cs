using Accounts.Persistence.Interfaces;
using Accounts.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Persistence.Extensions
{
    public static class PersistenceConfigure
    {
        public static IServiceCollection AddCustomDatabase(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<AccountsContext>(
                options =>
                    options.UseNpgsql(configuration.GetConnectionString("AccountsContext"))
            );

            services.AddTransient<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}