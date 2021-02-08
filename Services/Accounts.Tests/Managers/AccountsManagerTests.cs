using System.Collections.Generic;
using Accounts.Persistence.Models;
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
        private readonly List<AccountRecord> _accountEntities = new List<AccountRecord>
        {
            new AccountRecord
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0,
                LastSequentialNumber = 1
            },
            new AccountRecord
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToString(),
                Version = 0,
                LastSequentialNumber = 2
            },
            new AccountRecord
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
            var accountsRepositoryMock = new RepositoryMockFactory<AccountRecord>(_accountEntities).GetInstance();

            _manager = new AccountsManager(
                new Mock<ILogger<AccountsManager>>().Object,
                new AccountsService(accountsRepositoryMock.Object),
                publishEndpoint.Object
            );
        }

        #region ProcessAccountIsCheckedEventAsync

        [Fact]
        public async void Should_ProcessAccountIsCheckedEventAsync_Approved()
        {
            var accountApprovedEvent = new AccountIsCheckedEvent
            {
                Id = 1.ToGuid().ToString()
            };
            accountApprovedEvent.SetApproved();
            var newCreatedAccount = await _manager.ProcessAccountIsCheckedEventAsync(accountApprovedEvent);
            Assert.NotNull(newCreatedAccount);
            Assert.True(newCreatedAccount.IsApproved());
        }

        [Fact]
        public async void Should_ProcessAccountIsCheckedEventAsync_Blocked()
        {
            var accountApprovedEvent = new AccountIsCheckedEvent
            {
                Id = 1.ToGuid().ToString()
            };
            accountApprovedEvent.SetBlocked();
            var newCreatedAccount = await _manager.ProcessAccountIsCheckedEventAsync(accountApprovedEvent);
            Assert.NotNull(newCreatedAccount);
            Assert.True(newCreatedAccount.IsBlocked());
        }

        [Fact]
        public async void ShouldNot_ProcessAccountIsCheckedEventAsync_Invalid()
        {
            var accountApprovedEvent = new AccountIsCheckedEvent
            {
                Id = 5.ToGuid().ToString()
            };
            accountApprovedEvent.SetApproved();
            var newCreatedAccount = await _manager.ProcessAccountIsCheckedEventAsync(accountApprovedEvent);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region ProcessTransactionUpdatedEventAsync

        [Fact]
        public async void Should_ProcessTransactionUpdatedEventAsync_Valid()
        {
            var newTransaction = new TransactionCreatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 1.ToGuid().ToString(),
                Amount = 1,
                SequentialNumber = 2
            };
            newTransaction.SetApproved();
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(newTransaction);
            Assert.NotNull(newCreatedAccount);
            Assert.Equal(2, newCreatedAccount.Balance);
        }

        [Fact]
        public async void ShouldNot_ProcessTransactionUpdatedEventAsync_InvalidAccount()
        {
            var nonExistentAccountId = 4.ToGuid().ToString();
            var invalidTransaction = new TransactionUpdatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = nonExistentAccountId,
                Amount = 1,
                SequentialNumber = 3
            };
            invalidTransaction.SetApproved();
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(invalidTransaction);
            Assert.Null(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNot_ProcessTransactionUpdatedEventAsync_Declined()
        {
            var newTransaction = new TransactionCreatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 2.ToGuid().ToString(),
                Amount = 1,
                SequentialNumber = 3
            };
            newTransaction.SetDenied();
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(newTransaction);
            Assert.Null(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNot_ProcessTransactionUpdatedEventAsync_OutOfOrder()
        {
            var nonExistentAccountId = 4.ToGuid().ToString();
            var invalidTransaction = new TransactionUpdatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = nonExistentAccountId,
                Amount = 1,
                SequentialNumber = 10
            };
            invalidTransaction.SetApproved();
            var newCreatedAccount = await _manager.ProcessTransactionUpdatedEventAsync(invalidTransaction);
            Assert.Null(newCreatedAccount);
        }

        #endregion

        #region CreateNewAccountAsync

        [Fact]
        public async void Should_CreateNewAccountAsync_Valid()
        {
            var newCreatedAccount = await _manager.CreateNewAccountAsync("999");
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNot_CreateNewAccountAsync_Invalid()
        {
            var newCreatedAccount = await _manager.CreateNewAccountAsync(string.Empty);
            Assert.Null(newCreatedAccount);
        }

        #endregion
    }
}