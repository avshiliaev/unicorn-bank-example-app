using Accounts.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Persistence
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
        }

        public DbSet<AccountRecord> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRecord>().ToTable("Accounts");
            base.OnModelCreating(modelBuilder);
        }
    }
}