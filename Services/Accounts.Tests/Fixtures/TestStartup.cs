using System.Collections.Generic;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sdk.Persistence.Extensions;
using Sdk.Tests.Extensions;

namespace Accounts.Tests.Fixtures
{
    public class TestStartup : Startup
    {
        private readonly List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 3.ToGuid(),
                Balance = 1,
                ProfileId = 2.ToGuid(),
                Version = 0
            }
        };

        public TestStartup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly);
            services.RemoveAll(typeof(AccountsContext));
            services.RemoveAll(typeof(AccountsRepository));
            services.AddCustomDatabase<AccountsRepository, AccountEntity, InMemoryAccountsContext>(
                Configuration, "AccountsContext"
            );
        }
    }

    public class InMemoryAccountsContext: DbContext
    {
        public InMemoryAccountsContext(DbContextOptions<AccountsContext> options)
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

    public class IntegrationMemorySqlDbContext : DbContext
    {
        public IntegrationMemorySqlDbContext(DbContextOptions<IntegrationMemorySqlDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Test");
        }
    }
}