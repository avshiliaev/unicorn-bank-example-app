using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Accounts.Persistence
{
    public class AccountsContextFactory : IDesignTimeDbContextFactory<AccountsContext>
    {
        public AccountsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
            optionsBuilder.UseNpgsql(
                // configuration.GetConnectionString("AccountsContext")
                // TODO: fix it
                "User ID=postgres;Password=postgres;Host=localhost;Pooling=true;Database=test"
            );

            return new AccountsContext(optionsBuilder.Options);
        }
    }
}