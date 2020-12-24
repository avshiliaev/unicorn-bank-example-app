using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Extensions;

namespace Accounts.Persistence
{
    public class AccountsContextFactory : IDesignTimeDbContextFactory<AccountsContext>
    {
        public AccountsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
            optionsBuilder.BuildPostgreSqlConfiguration(
                "Accounts",
                "PostgreSql"
            );
            return new AccountsContext(optionsBuilder.Options);
        }
    }
}