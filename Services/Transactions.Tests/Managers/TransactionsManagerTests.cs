using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Transactions.Interfaces;
using Transactions.Managers;
using Transactions.Persistence.Entities;
using Transactions.Services;
using Xunit;

namespace Transactions.Tests.Managers
{
    public class TransactionsManagerTests
    {
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
        private readonly ITransactionsManager _manager;

        public TransactionsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<ITransactionModel>()
                .GetInstance();
            var transactionsRepositoryMock = new RepositoryMockFactory<TransactionEntity>(_transactionEntities)
                .GetInstance();

            _manager = new TransactionsManager(
                new Mock<ILogger<TransactionsManager>>().Object,
                new TransactionsService(transactionsRepositoryMock.Object), 
                publishEndpoint.Object
            );
        }

        #region UpdateStatusOfTransactionAsync

        [Fact]
        public async void ShouldSuccessfullyUpdateStatusOfTransactionAsync()
        {
            var transactionProcessedEvent = new TransactionProcessedEvent
            {
                Id = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Approved = true
            };
            var updatedTransaction = await _manager.UpdateStatusOfTransactionAsync(transactionProcessedEvent);
            Assert.NotNull(updatedTransaction);
            Assert.True(updatedTransaction.Approved);
        }

        [Fact]
        public async void ShouldNotUpdateStatusOfAnInvalidTransactionAsync()
        {
            var transactionProcessedEvent = new TransactionProcessedEvent
            {
                Id = 99.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Approved = true
            };
            var updatedTransaction = await _manager.UpdateStatusOfTransactionAsync(transactionProcessedEvent);
            Assert.NotNull(updatedTransaction);
        }

        #endregion

        #region CreateNewTransactionAsync

        [Fact]
        public async void ShouldSuccessfullyCreateANewTransaction()
        {
            var newCreatedAccount = await _manager.CreateNewTransactionAsync("999");
            Assert.NotNull(newCreatedAccount);
        }

        [Fact]
        public async void ShouldNotCreateAnInvalidAccount()
        {
            var newCreatedAccount = await _manager.CreateNewTransactionAsync(string.Empty);
            Assert.Null(newCreatedAccount);
        }

        #endregion
    }
}