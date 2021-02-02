using Billings.Persistence;
using Billings.Persistence.Entities;
using Billings.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Xunit;

namespace Billings.Tests.Repositories
{
    public class BillingsRepositoryTests
    {
        public class TestContext : BillingsContext
        {
            public TestContext(DbContextOptions<BillingsContext> options)
                : base(options)
            {
                ContextOptions = options;
            }

            protected DbContextOptions<BillingsContext> ContextOptions { get; }

            public DbSet<BillingEntity> Entities { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<BillingEntity>().ToTable("Entities");
                base.OnModelCreating(modelBuilder);
            }
        }

        public class AbstractRepositoryTests : TestContext
        {
            public AbstractRepositoryTests()
                : base(
                    new DbContextOptionsBuilder<BillingsContext>()
                        .UseInMemoryDatabase("InMemoryDbForTesting")
                        .Options
                )
            {
            }

            [Fact]
            public async void Should_AddAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var result = await repository.AddAsync(newEntity);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_ListAllAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var _ = await repository.AddAsync(newEntity);
                var result = await repository.ListAllAsync();
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }

            [Fact]
            public async void Should_GetByIdAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                var result = await repository.GetByIdAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetOneByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity {Version = 99};
                var _ = await repository.AddAsync(newEntity);

                var result = await repository.GetOneAsync(
                    e => e.Version == 99
                );
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_GetManyByParameterAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity {Version = 10};
                var _ = await repository.AddAsync(newEntity);

                var result = await repository.GetManyAsync(
                    e => e.Version == 10
                );
                Assert.Single(result);
            }

            [Fact]
            public async void Should_DeleteAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                var result = await repository.DeleteAsync(newEntitySaved.Id);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdateActivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);

                var result = await repository.UpdateOptimisticallyAsync(newEntitySaved);
                Assert.NotNull(result);
            }

            [Fact]
            public async void Should_UpdatePassivelyAsync_Valid()
            {
                await using var context = new TestContext(ContextOptions);
                var repository = new BillingsRepository(
                    new Mock<ILogger<AbstractRepository<BillingsContext, BillingEntity>>>().Object,
                    context
                );
                var newEntity = new BillingEntity();
                var newEntitySaved = await repository.AddAsync(newEntity);
                newEntitySaved.Version++;

                var result = await repository.UpdateAsync(newEntitySaved);
                Assert.NotNull(result);
            }
        }
    }
}