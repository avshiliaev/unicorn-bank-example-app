using Approvals.Persistence;
using Approvals.Persistence.Entities;
using Approvals.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Xunit;

namespace Approvals.Tests.Repositories
{
    public class ApprovalsRepositoryTests
    {
        public class TestContext : ApprovalsContext
        {
            public TestContext(DbContextOptions<ApprovalsContext> options)
                : base(options)
            {
                ContextOptions = options;
            }

            protected DbContextOptions<ApprovalsContext> ContextOptions { get; }

            public DbSet<ApprovalEntity> Entities { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ApprovalEntity>().ToTable("Entities");
                base.OnModelCreating(modelBuilder);
            }
        }

        public class AbstractRepositoryTests : TestContext
        {
            public AbstractRepositoryTests()
                : base(
                    new DbContextOptionsBuilder<ApprovalsContext>()
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .Options
                )
            {
            }

            [Fact]
            public async void Should_AddAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var result = await repository.AddAsync(newEntity);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_ListAllAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var _ = await repository.AddAsync(newEntity);
                var result = await repository.ListAllAsync();
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }

            [Fact]
            public async void Should_GetByIdAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                var result = await repository.GetByIdAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetOneByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity {Version = 99};
                var _ = await repository.AddAsync(newEntity);

                var result = await repository.GetOneByParameterAsync(
                    e => e.Version == 99
                );
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetManyByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity {Version = 10};
                var _ = await repository.AddAsync(newEntity);

                var result = await repository.GetManyByParameterAsync(
                    e => e.Version == 10
                );
                Assert.Single(result);
            }

            [Fact]
            public async void Should_DeleteAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                var result = await repository.DeleteAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdateActivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);

                var result = await repository.UpdateActivelyAsync(newEntitySaved);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdatePassivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new ApprovalsRepository(
                    new Mock<ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>>>().Object,
                    context
                );
                var newEntity = new ApprovalEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                newEntitySaved.Version++;

                var result = await repository.UpdatePassivelyAsync(newEntitySaved);
                Assert.NotNull(result);
            }
        }
    }
}