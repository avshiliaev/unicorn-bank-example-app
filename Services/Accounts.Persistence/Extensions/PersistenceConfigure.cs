using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;

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

            services.AddTransient<IRepository<AccountEntity>, AccountsRepository>();

            return services;
        }
    }
}