using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Extensions;

namespace Billings.Persistence
{
    public class BillingsContextFactory : IDesignTimeDbContextFactory<BillingsContext>
    {
        public BillingsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BillingsContext>();
            optionsBuilder.BuildPostgreSqlConfiguration(
                "Billings",
                "PostgreSql"
            );
            return new BillingsContext(optionsBuilder.Options);
        }
    }
}