using Approvals.Persistence;
using Approvals.Persistence.Models;
using Approvals.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Xunit;

namespace Approvals.Tests.Repositories
{
    public class AccountsRepositoryTests
    {
        public class TestContext : ApprovalsContext
        {
            public TestContext(DbContextOptions<ApprovalsContext> options)
                : base(options)
            {
                ContextOptions = options;
            }

            protected DbContextOptions<ApprovalsContext> ContextOptions { get; }

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
                    new DbContextOptionsBuilder<ApprovalsContext>()
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .ConfigureWarnings(
                            x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                        )
                        .Options
                )
            {
            }

            [Fact]
            public async void Should_AddAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractEventRepository<ApprovalsContext, AccountRecord>>>().Object,
                    context
                );
                var newEntity = new AccountRecord();
                var result = await repository.AppendStateOfEntity(newEntity);
                Assert.NotNull(result);
            }
        }
    }
}