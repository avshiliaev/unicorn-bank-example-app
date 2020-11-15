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

        public DbSet<AccountModel> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().ToTable("Accounts");
            base.OnModelCreating(modelBuilder);
        }
    }
}