using System;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;
using Xunit;

namespace Accounts.Tests.Repositories
{
    public class AccountsRepositoryTests
    {
        
    public class TestContext : AccountsContext
    {
        protected DbContextOptions<AccountsContext> ContextOptions { get; }
        
        public TestContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
            ContextOptions = options;
        }

        public DbSet<AccountEntity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Entities");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AbstractRepositoryTests : TestContext
    {
        public AbstractRepositoryTests()
            : base(
                new DbContextOptionsBuilder<AccountsContext>()
                    .UseInMemoryDatabase("InMemoryDbForTesting")
                    .Options
                )
        {
        }
        
        [Fact]
        public async void Should_AddAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var result = await repository.AddAsync(newEntity);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void Should_ListAllAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var _ = await repository.AddAsync(newEntity);
            var result = await repository.ListAllAsync();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public async void Should_GetByIdAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            var result = await repository.GetByIdAsync(newEntitySaved.Id);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void Should_GetOneByParameterAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity{Version = 99};
            var _ = await repository.AddAsync(newEntity);
            
            var result = await repository.GetOneByParameterAsync(
                (e) =>  e.Version == 99
                );
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void Should_GetManyByParameterAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity{Version = 10};
            var _ = await repository.AddAsync(newEntity);
            
            var result = await repository.GetManyByParameterAsync(
                (e) =>  e.Version == 10
                );
            Assert.Single(result);
        }
        
        [Fact]
        public async void Should_DeleteAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            var result = await repository.DeleteAsync(newEntitySaved.Id);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void Should_UpdateActivelyAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);

            var result = await repository.UpdateActivelyAsync(newEntitySaved);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void Should_UpdatePassivelyAsync_Valid()
        {
            await using var context = new TestContext(ContextOptions);
            var repository = new AccountsRepository(
                new Mock<ILogger<AbstractRepository<AccountsContext, AccountEntity>>>().Object,
                context
            );
            var newEntity = new AccountEntity();
            var newEntitySaved = await repository.AddAsync(newEntity);
            newEntitySaved.Version++;

            var result = await repository.UpdatePassivelyAsync(newEntitySaved);
            Assert.NotNull(result);
        }
    }
    }
}