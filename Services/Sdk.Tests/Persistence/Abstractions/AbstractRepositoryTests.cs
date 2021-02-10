using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Interfaces;
using Sdk.Persistence.Abstractions;
using Xunit;

namespace Sdk.Tests.Persistence.Abstractions
{
    public class TestRecord : IRecord
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string ProfileId { get; set; }
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

        public DbSet<TestRecord> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestRecord>().ToTable("Entities");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class TestRepository : AbstractEventRepository<TestContext, TestRecord>
    {
        public TestRepository(
            ILogger<AbstractEventRepository<TestContext, TestRecord>> logger,
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
                new Mock<ILogger<AbstractEventRepository<TestContext, TestRecord>>>().Object,
                context
            );
            var newEntity = new TestRecord();
            var result = await repository.AppendStateOfEntity(newEntity);
            Assert.NotNull(result);
        }
    }
}