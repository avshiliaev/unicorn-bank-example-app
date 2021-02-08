using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Extensions;
using Sdk.Persistence.Tools;

namespace Transactions.Persistence
{
    public class TransactionsContextFactory : IDesignTimeDbContextFactory<TransactionsContext>
    {
        public TransactionsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransactionsContext>();
            optionsBuilder.BuildPostgreSqlConfiguration(
                "Transactions",
                "PostgreSql"
            );
            return new TransactionsContext(optionsBuilder.Options);
        }
    }
}