using Microsoft.EntityFrameworkCore;
using Transactions.Persistence.Entities;

namespace Transactions.Persistence
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext(DbContextOptions<TransactionsContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEntity>().ToTable("Transactions");
            base.OnModelCreating(modelBuilder);
        }
    }
}