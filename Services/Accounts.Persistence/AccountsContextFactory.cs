using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Extensions;
using Sdk.Persistence.Tools;

namespace Accounts.Persistence
{
    public class AccountsContextFactory : IDesignTimeDbContextFactory<AccountsContext>
    {
        public AccountsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
            var options = PostgreSqlConfigurationBuilder.BuildPostgreSqlConfiguration<AccountsContext>(
                optionsBuilder,
                "Accounts",
                "PostgreSql"
            );
            return new AccountsContext(options);
        }
    }
}