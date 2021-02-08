using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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