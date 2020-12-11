using System;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Managers;
using Accounts.Tests.Extensions;
using Accounts.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
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

        #region UpdateExistingAccountAsync

        [Fact]
        public async void ShouldSuccessfullyUpdateExistingAccount()
        {
            var newAccountDto = new AccountDto
            {
                Id = 0.ToGuid().ToString(),
                Balance = 3
            };
            var newCreatedAccount = await _manager.UpdateExistingAccountAsync(newAccountDto);
            Assert.NotNull(newCreatedAccount);
            Assert.Equal(3, newCreatedAccount.Balance);
        }

        [Fact]
        public async void ShouldNotUpdateAnInvalidAccount()
        {
            var newAccountDto = new AccountDto
            {
                Id = 5.ToGuid().ToString(),
                Balance = 3
            };
            var newCreatedAccount = await _manager.UpdateExistingAccountAsync(newAccountDto);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region AddTransactionToAccountAsync

        [Fact]
        public async void ShouldSuccessfullyAddTransactionToAccount()
        {
            var newTransaction = new TransactionCreatedEvent
            {
                Id = 0.ToGuid().ToString(),
                AccountId = 0.ToGuid().ToString(),
                Amount = 1
            };
            var newCreatedAccount = await _manager.AddTransactionToAccountAsync(newTransaction);
            Assert.NotNull(newCreatedAccount);
            Assert.Equal(2, newCreatedAccount.Balance);
        }

        [Fact]
        public async void ShouldNotAddInvalidTransactionToAccount()
        {
            var nonExistentAccountId = 4.ToGuid().ToString();
            var invalidTransaction = new TransactionCreatedEvent
            {
                Id = 0.ToGuid().ToString(),
                AccountId = nonExistentAccountId,
                Amount = 1
            };
            var newCreatedAccount = await _manager.AddTransactionToAccountAsync(invalidTransaction);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region CreateNewAccountAsync

        [Fact]
        public async void ShouldSuccessfullyCreateANewAccount()
        {
            var newValidAccountDto = new AccountDto
            {
                ProfileId = Guid.NewGuid().ToString()
            };
            var newCreatedAccount = await _manager.CreateNewAccountAsync(newValidAccountDto);
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNotCreateAnInvalidAccount()
        {
            var newInvalidAccountDto = new AccountDto
            {
                Balance = 3
            };
            var newCreatedAccount = await _manager.CreateNewAccountAsync(newInvalidAccountDto);
            Assert.Null(newCreatedAccount);
        }

        #endregion
    }
}