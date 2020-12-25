using System.Collections.Generic;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Transactions.Interfaces;
using Transactions.Persistence.Entities;
using Transactions.Services;
using Xunit;

namespace Transactions.Tests.Services
{
    public class TransactionsServiceTests
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

        private readonly ITransactionsService _service;

        public TransactionsServiceTests()
        {
            var transactionsRepositoryMock = new RepositoryMockFactory<TransactionEntity>(_transactionEntities)
                .GetInstance();
            _service = new TransactionsService(transactionsRepositoryMock.Object);
        }

        [Fact]
        public async void ShouldSuccessfullyCreateANewTransaction()
        {
            var newTransactionEntity = new TransactionEntity
            {
                Amount = 1,
                Approved = true,
                ProfileId = 1.ToString(),
                Version = 0
            };
            var newCreatedTransactionsEntity = await _service.CreateTransactionAsync(newTransactionEntity);
            Assert.NotNull(newCreatedTransactionsEntity);
        }

        [Fact]
        public async void ShouldActivelyUpdateExistingTransactions()
        {
            var transactionEntity = new TransactionEntity
            {
                Id = 1.ToGuid(),
                Amount = 1,
                Approved = true,
                ProfileId = 1.ToString(),
                Version = 0
            };
            var updatedTransactionEntity = await _service.UpdateTransactionAsync(transactionEntity);
            Assert.NotNull(updatedTransactionEntity);
            Assert.Equal(1, updatedTransactionEntity.Version);
        }

        [Fact]
        public async void ShouldNotUpdateAnInvalidTransactions()
        {
            var invalidTransactionsEntity = new TransactionEntity
            {
                Id = 99.ToGuid(),
                Amount = 1,
                Approved = true,
                ProfileId = 1.ToString(),
                Version = 0
            };
            var updatedTransactionEntity = await _service.UpdateTransactionAsync(invalidTransactionsEntity);
            Assert.Null(updatedTransactionEntity);
        }

        [Fact]
        public async void ShouldNotUpdateAnAccountWithWrongVersion()
        {
            var transactionEntity = new TransactionEntity
            {
                Id = 1.ToGuid(),
                Amount = 1,
                Approved = true,
                ProfileId = 1.ToString(),
                Version = 3
            };
            var updatedTransactionEntity = await _service.UpdateTransactionAsync(transactionEntity);
            Assert.Null(updatedTransactionEntity);
        }
    }
}