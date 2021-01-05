using System.Collections.Generic;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Services;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Accounts.Tests.Services
{
    public class AccountsServiceTests
    {
        private readonly List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 3.ToGuid(),
                Balance = 1,
                ProfileId = 2.ToString(),
                Version = 0
            }
        };

        private readonly IAccountsService _service;

        public AccountsServiceTests()
        {
            var accountsRepositoryMock = new RepositoryMockFactory<AccountEntity>(_accountEntities).GetInstance();
            _service = new AccountsService(accountsRepositoryMock.Object);
        }

        [Fact]
        public async void Should_CreateAccountAsync_Valid()
        {
            var newAccountEntity = new AccountEntity
            {
                ProfileId = "999"
            };
            var newCreatedAccountEntity = await _service.CreateAccountAsync(newAccountEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }

        [Fact]
        public async void Should_UpdateAccountAsync_Valid()
        {
            var accountEntity = new AccountEntity
            {
                Id = 1.ToGuid(),
                ProfileId = 1.ToString(),
                Balance = 100,
                Version = 0
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(accountEntity);
            Assert.NotNull(updatedAccountEntity);
            Assert.Equal(1, updatedAccountEntity.Version);
        }

        [Fact]
        public async void ShouldNot_UpdateAccountAsync_Invalid()
        {
            var invalidAccountEntity = new AccountEntity
            {
                Id = 5.ToGuid(),
                ProfileId = 1.ToString(),
                Balance = 100,
                Version = 1
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(invalidAccountEntity);
            Assert.Null(updatedAccountEntity);
        }

        [Fact]
        public async void ShouldNot_UpdateAccountAsync_OutOfOrder()
        {
            var invalidAccountEntity = new AccountEntity
            {
                Id = 1.ToGuid(),
                ProfileId = 1.ToString(),
                Balance = 100,
                Version = 3
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(invalidAccountEntity);
            Assert.Null(updatedAccountEntity);
        }
    }
}