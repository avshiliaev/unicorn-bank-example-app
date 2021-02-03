using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;
using Xunit;

namespace Sdk.Tests.Persistence.Abstractions
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }

    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
            ContextOptions = options;
        }

        protected DbContextOptions<TestContext> ContextOptions { get; }

        public DbSet<TestEntity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>().ToTable("Entities");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class TestRepository : AbstractEventRepository<TestContext, TestEntity>
    {
        public TestRepository(
            ILogger<AbstractEventRepository<TestContext, TestEntity>> logger,
            TestContext context
        ) : base(logger, context)
        {
        }
    }

    public class AbstractRepositoryTests : TestContext
    {
        public AbstractRepositoryTests()
            : base(
                new DbContextOptionsBuilder<TestContext>()
                    .UseInMemoryDatabase("InMemoryDbForTesting")
                    .Options
            )
        {
        }

        [Fact]
        public async void Should_AddAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var result = await repository.AddAsync(newEntity);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Should_ListAllAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var _ = await repository.AddAsync(newEntity);
            var result = await repository.ListAllAsync();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Should_GetByIdAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            var result = await repository.GetByIdAsync(newEntitySaved.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Should_GetOneByParameterAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity {Version = 99};
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
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity {Version = 10};
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
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            var result = await repository.DeleteAsync(newEntitySaved.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Should_UpdateActivelyAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);

            var result = await repository.UpdateOptimisticallyAsync(newEntitySaved);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Should_UpdatePassivelyAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new TestRepository(
                new Mock<ILogger<AbstractEventRepository<TestContext, TestEntity>>>().Object,
                context
            );
            var newEntity = new TestEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            newEntitySaved.Version++;

            var result = await repository.UpdateAsync(newEntitySaved);
            Assert.NotNull(result);
        }
    }
}