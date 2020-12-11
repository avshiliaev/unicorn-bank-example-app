using System;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Services;
using Accounts.Tests.Extensions;
using Accounts.Tests.Mocks;
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
                Id = 0.ToGuid(),
                ProfileId = 0.ToGuid(),
                Balance = 100
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
                Balance = 100
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(invalidAccountEntity);
            Assert.Null(updatedAccountEntity);
        }
    }
}