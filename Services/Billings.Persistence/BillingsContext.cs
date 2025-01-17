using Billings.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence
{
    public class BillingsContext : DbContext
    {
        public BillingsContext(DbContextOptions<BillingsContext> options)
            : base(options)
        {
        }

        public DbSet<BillingEntity> Billings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingEntity>().ToTable("Billings");
            base.OnModelCreating(modelBuilder);
        }
    }
}