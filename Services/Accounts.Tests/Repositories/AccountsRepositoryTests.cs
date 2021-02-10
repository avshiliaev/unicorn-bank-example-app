using Accounts.Persistence;
using Accounts.Persistence.Models;
using Accounts.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Xunit;

namespace Accounts.Tests.Repositories
{
    public class AccountsRepositoryTests
    {
        public class TestContext : AccountsContext
        {
            public TestContext(DbContextOptions<AccountsContext> options)
                : base(options)
            {
                ContextOptions = options;
            }

            protected DbContextOptions<AccountsContext> ContextOptions { get; }

            public DbSet<AccountRecord> Entities { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<AccountRecord>().ToTable("Entities");
                base.OnModelCreating(modelBuilder);
            }
        }

        public class AbstractRepositoryTests : TestContext
        {
            public AbstractRepositoryTests()
                : base(
                    new DbContextOptionsBuilder<AccountsContext>()
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                        .Options
                )
            {
            }

            [Fact]
            public async void Should_AddAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new AccountsRepository(
                    new Mock<ILogger<AbstractEventRepository<AccountsContext, AccountRecord>>>().Object,
                    context
                );
                var newEntity = new AccountRecord();
                var result = await repository.AppendStateOfEntity(newEntity);
                Assert.NotNull(result);
            }
        }
    }
}