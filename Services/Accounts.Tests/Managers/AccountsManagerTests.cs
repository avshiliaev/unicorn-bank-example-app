using System;
using System.Collections.Generic;
using Accounts.Interfaces;
using Accounts.Managers;
using Accounts.Persistence.Entities;
using Accounts.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
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
            var accountApprovedEvent = new AccountApprovedEvent
            {
                Id = 1.ToGuid().ToString(),
                Approved = true
            };
            var newCreatedAccount = await _manager.AddApprovalToAccountAsync(accountApprovedEvent);
            Assert.NotNull(newCreatedAccount);
            Assert.True(newCreatedAccount.Approved);
        }

        [Fact]
        public async void ShouldNotAddApprovalToAnInvalidAccount()
        {
            var accountApprovedEvent = new AccountApprovedEvent
            {
                Id = 5.ToGuid().ToString(),
                Approved = true
            };
            var newCreatedAccount = await _manager.AddApprovalToAccountAsync(accountApprovedEvent);
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
                Id = 1.ToGuid().ToString(),
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