using System.Collections.Generic;
using Accounts.Interfaces;
using Accounts.Managers;
using Accounts.Persistence.Entities;
using Accounts.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Accounts.Tests.Managers
{
    public class AccountsManagerTests
    {
        private readonly List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0,
                LastSequentialNumber = 1
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0,
                LastSequentialNumber = 2
            },
            new AccountEntity
            {
                Id = 3.ToGuid(),
                Balance = 1,
                ProfileId = 2.ToString(),
                Version = 0,
                LastSequentialNumber = 3
            }
        };

        private readonly IAccountsManager _manager;

        public AccountsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<IAccountModel>().GetInstance();
            var accountsRepositoryMock = new RepositoryMockFactory<AccountEntity>(_accountEntities).GetInstance();

            _manager = new AccountsManager(
                new Mock<ILogger<AccountsManager>>().Object,
                new AccountsService(accountsRepositoryMock.Object),
                publishEndpoint.Object
            );
        }

        #region AddApprovalToAccountAsync

        [Fact]
        public async void ShouldSuccessfullyAddApprovalToAccountAsync()
        {
            var accountApprovedEvent = new AccountIsCheckedEvent
            {
                Id = 1.ToGuid().ToString()
            };
            accountApprovedEvent.SetApproval();
            var newCreatedAccount = await _manager.ProcessAccountIsCheckedEventAsync(accountApprovedEvent);
            Assert.NotNull(newCreatedAccount);
            Assert.True(newCreatedAccount.Approved);
        }

        [Fact]
        public async void ShouldNotAddApprovalToAnInvalidAccount()
        {
            var accountApprovedEvent = new AccountIsCheckedEvent
            {
                Id = 5.ToGuid().ToString(),
                Approved = true
            };
            var newCreatedAccount = await _manager.ProcessAccountIsCheckedEventAsync(accountApprovedEvent);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region AddTransactionToAccountAsync

        [Fact]
        public async void ShouldSuccessfullyAddTransactionToAccount()
        {
            var newTransaction = new TransactionCreatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 1.ToGuid().ToString(),
                Amount = 1,
                Approved = true,
                Pending = false,
                SequentialNumber = 2
            };
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(newTransaction);
            Assert.NotNull(newCreatedAccount);
            Assert.Equal(2, newCreatedAccount.Balance);
        }

        [Fact]
        public async void ShouldNotAddInvalidTransactionToAccount()
        {
            var nonExistentAccountId = 4.ToGuid().ToString();
            var invalidTransaction = new TransactionUpdatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = nonExistentAccountId,
                Amount = 1,
                Approved = true,
                Pending = false,
                SequentialNumber = 3
            };
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(invalidTransaction);
            Assert.Null(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNotAddDeclinedTransactionToAccount()
        {
            var newTransaction = new TransactionCreatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 1.ToGuid().ToString(),
                Amount = 1,
                Approved = false,
                Pending = false,
                SequentialNumber = 3
            };
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(newTransaction);
            Assert.Null(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNotAddTransactionToAccountOutOfOrder()
        {
            var nonExistentAccountId = 4.ToGuid().ToString();
            var invalidTransaction = new TransactionUpdatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = nonExistentAccountId,
                Amount = 1,
                Approved = true,
                Pending = false,
                SequentialNumber = 10
            };
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(invalidTransaction);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region CreateNewAccountAsync

        [Fact]
        public async void ShouldSuccessfullyCreateANewAccount()
        {
            var newCreatedAccount = await _manager.CreateNewAccountAsync("999");
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNotCreateAnInvalidAccount()
        {
            var newCreatedAccount = await _manager.CreateNewAccountAsync(string.Empty);
            Assert.Null(newCreatedAccount);
        }

        #endregion
    }
}