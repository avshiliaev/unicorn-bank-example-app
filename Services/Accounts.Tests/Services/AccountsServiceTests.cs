using System;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Services;
using Accounts.Tests.Mocks;
using Sdk.Tests.Extensions;
using Xunit;

namespace Accounts.Tests.Services
{
    public class AccountsServiceTests
    {
        private readonly IAccountsService _service;

        public AccountsServiceTests()
        {
            var accountsRepositoryMock = new AccountsRepositoryMockFactory().GetInstance();
            _service = new AccountsService(accountsRepositoryMock.Object);
        }

        [Fact]
        public async void ShouldSuccessfullyCreateANewAccount()
        {
            var newAccountEntity = new AccountEntity()
            {
                ProfileId = Guid.NewGuid()
            };
            var newCreatedAccountEntity = await _service.CreateAccountAsync(newAccountEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }
        
        [Fact]
        public async void ShouldSuccessfullyUpdateExistingAccount()
        {
            var accountEntity = new AccountEntity()
            {
                Id = 1.ToGuid(),
                ProfileId = 1.ToGuid(),
                Balance = 100,
                Version = 1
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(accountEntity);
            Assert.NotNull(updatedAccountEntity);
        }
        
        [Fact]
        public async void ShouldNotUpdateAnInvalidAccount()
        {
            var invalidAccountEntity = new AccountEntity()
            {
                Id = 5.ToGuid(),
                ProfileId = 1.ToGuid(),
                Balance = 100,
                Version = 1
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(invalidAccountEntity);
            Assert.Null(updatedAccountEntity);
        }
        
        [Fact]
        public async void ShouldNotUpdateAnAccountWithWrongVersion()
        {
            var invalidAccountEntity = new AccountEntity()
            {
                Id = 1.ToGuid(),
                ProfileId = 1.ToGuid(),
                Balance = 100,
                Version = 3
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(invalidAccountEntity);
            Assert.Null(updatedAccountEntity);
        }
    }
}