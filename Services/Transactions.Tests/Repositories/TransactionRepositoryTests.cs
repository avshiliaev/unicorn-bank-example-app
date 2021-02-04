using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Transactions.Persistence;
using Transactions.Persistence.Entities;
using Transactions.Persistence.Repositories;
using Xunit;

namespace Transactions.Tests.Repositories
{
    public class TransactionsRepositoryTests
    {
        public class TestContext : TransactionsContext
        {
            public TestContext(DbContextOptions<TransactionsContext> options)
                : base(options)
            {
                ContextOptions = options;
            }

            protected DbContextOptions<TransactionsContext> ContextOptions { get; }

            public DbSet<TransactionEntity> Entities { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TransactionEntity>().ToTable("Entities");
                base.OnModelCreating(modelBuilder);
            }
        }

        public class AbstractRepositoryTests : TestContext
        {
            public AbstractRepositoryTests()
                : base(
                    new DbContextOptionsBuilder<TransactionsContext>()
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .Options
                )
            {
            }

            [Fact]
            public async void Should_AddAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var result = await repository.AppendStateOfEntity(newEntity);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_ListAllAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var _ = await repository.AppendStateOfEntity(newEntity);
                var result = await repository.ListAllAsync();
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }

            [Fact]
            public async void Should_GetByIdAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var newEntitySaved = await repository.AppendStateOfEntity(newEntity);
                var result = await repository.GetByIdAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetOneByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity {Version = 99};
                var _ = await repository.AppendStateOfEntity(newEntity);

                var result = await repository.GetOneEntityLastStateAsync(
                    e => e.Version == 99
                );
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetManyByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity {Version = 10};
                var _ = await repository.AppendStateOfEntity(newEntity);

                var result = await repository.GetManyAsync(
                    e => e.Version == 10
                );
                Assert.Single(result);
            }

            [Fact]
            public async void Should_DeleteAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var newEntitySaved = await repository.AppendStateOfEntity(newEntity);
                var result = await repository.DeleteAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdateActivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var newEntitySaved = await repository.AppendStateOfEntity(newEntity);

                var result = await repository.UpdateOptimisticallyAsync(newEntitySaved);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdatePassivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new TransactionsRepository(
                    new Mock<ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>>>().Object,
                    context
                );
                var newEntity = new TransactionEntity();
                var newEntitySaved = await repository.AppendStateOfEntity(newEntity);
                newEntitySaved.Version++;

                var result = await repository.UpdateAsync(newEntitySaved);
                Assert.NotNull(result);
            }
        }
    }
}