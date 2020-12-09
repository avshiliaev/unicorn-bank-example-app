using System;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Services;
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
        public async void CreateAccountAsyncTest()
        {
            var newAccountEntity = new AccountEntity()
            {
                ProfileId = Guid.NewGuid()
            };
            var newCreatedAccountEntity = await _service.CreateAccountAsync(newAccountEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }
        
        [Fact]
        public async void UpdateAccountAsyncTest()
        {
            var accountEntity = new AccountEntity()
            {
                ProfileId = Guid.NewGuid(),
                Balance = 100
            };
            var updatedAccountEntity = await _service.UpdateAccountAsync(accountEntity);
            Assert.NotNull(updatedAccountEntity);
        }
    }
}