using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Dto;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Transactions.Interfaces;
using Transactions.Managers;
using Transactions.Mappers;
using Transactions.Persistence.Entities;
using Transactions.Services;
using Transactions.Tests.Mocks;
using Transactions.ViewModels;
using Xunit;

namespace Transactions.Tests.Managers
{
    public class TransactionsManagerTests
    {
        private readonly ITransactionsManager _manager;

        private readonly List<TransactionEntity> _transactionEntities = new List<TransactionEntity>
        {
            new TransactionEntity
            {
                Id = 1.ToGuid(),
                Amount = 1,
                ProfileId = 1.ToString(),
                Version = 0
            },
            new TransactionEntity
            {
                Id = 2.ToGuid(),
                Amount = 1,
                ProfileId = 1.ToString(),
                Version = 0
            },
            new TransactionEntity
            {
                Id = 3.ToGuid(),
                Amount = 1,
                ProfileId = 2.ToString(),
                Version = 0
            }
        };

        public TransactionsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<ITransactionModel>()
                .GetInstance();
            var transactionsRepositoryMock = new RepositoryMockFactory<TransactionEntity>(_transactionEntities)
                .GetInstance();
            var concurrencyManagerMock = new ConcurrencyManagerMockFactory(_transactionEntities).GetInstance();
            var licenseManagerMock = new LicenseManagerMockFactory<ITransactionModel>().GetInstance();

            _manager = new TransactionsManager(
                new Mock<ILogger<TransactionsManager>>().Object,
                new TransactionsService(transactionsRepositoryMock.Object),
                publishEndpoint.Object,
                concurrencyManagerMock.Object,
                licenseManagerMock.Object
            );
        }

        #region ProcessTransactionCheckedEventAsync

        [Fact]
        public async void Should_ProcessTransactionCheckedEventAsync_Approved()
        {
            var transactionProcessedEvent = new TransactionIsCheckedEvent
            {
                Id = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString()
            };
            transactionProcessedEvent.SetApproved();
            var updatedTransaction = await _manager.ProcessTransactionCheckedEventAsync(transactionProcessedEvent);
            Assert.NotNull(updatedTransaction);
            Assert.True(updatedTransaction.IsApproved());
        }

        [Fact]
        public async void Should_ProcessTransactionCheckedEventAsync_Blocked()
        {
            var transactionProcessedEvent = new TransactionIsCheckedEvent
            {
                Id = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString()
            };
            transactionProcessedEvent.SetBlocked();
            var updatedTransaction = await _manager.ProcessTransactionCheckedEventAsync(transactionProcessedEvent);
            Assert.NotNull(updatedTransaction);
            Assert.True(updatedTransaction.IsBlocked());
        }

        [Fact]
        public async void ShouldNot_ProcessTransactionCheckedEventAsync_Invalid()
        {
            var transactionProcessedEvent = new TransactionIsCheckedEvent
            {
                Id = 99.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString()
            };
            transactionProcessedEvent.SetApproved();
            var updatedTransaction = await _manager.ProcessTransactionCheckedEventAsync(transactionProcessedEvent);
            Assert.Null(updatedTransaction);
        }

        #endregion

        #region CreateNewTransactionAsync

        [Fact]
        public async void Should_CreateNewTransactionAsync_Valid()
        {
            var transactionViewModel = new TransactionViewModel
            {
                AccountId = 1.ToGuid().ToString(),
                Amount = 0f,
                Info = "Info"
            };
            var transactionDto = transactionViewModel.ToTransactionModel<TransactionDto>(1.ToString());

            var newCreatedAccount = await _manager.CreateNewTransactionAsync(transactionDto);
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNot_CreateNewTransactionAsync_Invalid()
        {
            var transactionViewModel = new TransactionViewModel
            {
                AccountId = 0.ToGuid().ToString(),
                Amount = 0f,
                Info = "Info"
            };
            var transactionDto = transactionViewModel.ToTransactionModel<TransactionDto>(1.ToGuid().ToString());

            var newCreatedAccount = await _manager.CreateNewTransactionAsync(transactionDto);
            Assert.Null(newCreatedAccount);
        }

        #endregion
    }
}