using System.Reflection;
using Accounts.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Persistence
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Accounts");
            base.OnModelCreating(modelBuilder);
        }
    }
}