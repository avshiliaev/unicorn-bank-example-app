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

        public DbSet<AccountEntity> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Approvals");
            base.OnModelCreating(modelBuilder);
        }
    }
}