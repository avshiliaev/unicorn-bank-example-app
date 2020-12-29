using Approvals.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Approvals.Persistence
{
    public class ApprovalsContext : DbContext
    {
        public ApprovalsContext(DbContextOptions<ApprovalsContext> options)
            : base(options)
        {
        }

        public DbSet<ApprovalEntity> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalEntity>().ToTable("Approvals");
            base.OnModelCreating(modelBuilder);
        }
    }
}