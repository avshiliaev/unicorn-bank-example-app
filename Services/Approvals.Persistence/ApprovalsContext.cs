using Approvals.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Approvals.Persistence
{
    public class ApprovalsContext : DbContext
    {
        public ApprovalsContext(DbContextOptions<ApprovalsContext> options)
            : base(options)
        {
        }

        public DbSet<AccountRecord> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRecord>().ToTable("Approvals");
            base.OnModelCreating(modelBuilder);
        }
    }
}