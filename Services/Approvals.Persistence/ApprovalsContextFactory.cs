using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sdk.Persistence.Tools;

namespace Approvals.Persistence
{
    public class ApprovalsContextFactory : IDesignTimeDbContextFactory<ApprovalsContext>
    {
        public ApprovalsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApprovalsContext>();
            var options = PostgreSqlConfigurationBuilder.BuildPostgreSqlConfiguration(
                optionsBuilder,
                "Approvals",
                "PostgreSql"
            );
            return new ApprovalsContext(options);
        }
    }
}