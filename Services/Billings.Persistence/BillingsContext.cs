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

        public DbSet<TransactionEntity> Billings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEntity>().ToTable("Billings");
            base.OnModelCreating(modelBuilder);
        }
    }
}