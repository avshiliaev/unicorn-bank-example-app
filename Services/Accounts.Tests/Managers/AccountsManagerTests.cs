using System;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Managers;
using Accounts.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Accounts.Tests.Managers
{
    public class AccountsManagerTests
    {
        private readonly IAccountsManager _manager;

        public AccountsManagerTests()
        {
            var accountsService = new AccountsServiceMockFactory().GetInstance();
            var publishEndpoint = new PublishEndpointMockFactory().GetInstance();
            _manager = new AccountsManager(
                new Mock<ILogger<AccountsManager>>().Object,
                accountsService.Object,
                publishEndpoint.Object
            );
        }

        [Fact]
        public async void CreateNewAccountAsyncTest()
        {
            var newAccountDto = new AccountDto
            {
                ProfileId = Guid.NewGuid().ToString()
            };
            var newCreatedAccount = await _manager.CreateNewAccountAsync(newAccountDto);
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void UpdateExistingAccountAsyncTest()
        {
            var newAccountDto = new AccountDto
            {
                ProfileId = Guid.NewGuid().ToString(),
                Balance = 100
            };
            var newCreatedAccount = await _manager.UpdateExistingAccountAsync(newAccountDto);
            Assert.NotNull(newCreatedAccount);
        }
    }
}