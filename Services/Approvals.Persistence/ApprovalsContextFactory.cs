using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Extensions;

namespace Approvals.Persistence
{
    public class ApprovalsContextFactory : IDesignTimeDbContextFactory<ApprovalsContext>
    {
        public ApprovalsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApprovalsContext>();
            optionsBuilder.BuildPostgreSqlConfiguration(
                "Approvals",
                "PostgreSql"
            );
            return new ApprovalsContext(optionsBuilder.Options);
        }
    }
}