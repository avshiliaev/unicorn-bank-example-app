using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Concurrency.Extensions;
using Sdk.Extensions;
using Transactions.Interfaces;
using Transactions.Mappers;

namespace Transactions.Managers
{
    public class TransactionsManager : ITransactionsManager
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITransactionsService _transactionsService;
        private readonly IConcurrencyManager _concurrencyManager;

        public TransactionsManager(
            ILogger<TransactionsManager> logger,
            ITransactionsService transactionsService,
            IPublishEndpoint publishEndpoint,
            IConcurrencyManager concurrencyManager
        )
        {
            _transactionsService = transactionsService;
            _publishEndpoint = publishEndpoint;
            _concurrencyManager = concurrencyManager;
        }

        public async Task<TransactionDto?> CreateNewTransactionAsync(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                transactionModel.AccountId == Guid.Empty.ToString()
            )
                return null;

            // Optimistic Concurrency Control: set the sequential number
            var sequencedTransaction = await _concurrencyManager.SetNextSequentialNumber(transactionModel);

            var newTransaction = await _transactionsService.CreateTransactionAsync(
                sequencedTransaction.ToTransactionEntity()
            );

            if (newTransaction != null)
            {
                await _publishEndpoint.Publish(newTransaction.ToTransactionModel<TransactionCreatedEvent>());
                return newTransaction.ToTransactionModel<TransactionDto>();    
            }
            return null;

        }

        public async Task<TransactionDto?> ProcessTransactionProcessedEventAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = await _transactionsService.GetTransactionByIdAsync(transactionModel.Id.ToGuid());
            if (transactionEntity != null)
            {
                if (transactionModel.IsApproved())
                    transactionEntity.SetApproval();
                else
                    transactionEntity.SetDenial();
                
                // Optimistic Concurrency Control: increment the version on update
                var updatedTransaction = await _transactionsService.UpdateTransactionAsync(transactionEntity);
                if (updatedTransaction != null)
                {
                    await _publishEndpoint.Publish(
                        updatedTransaction.ToTransactionModel<TransactionUpdatedEvent>()
                    );
                    await _publishEndpoint.Publish(updatedTransaction.ToNotificationEvent());
                    return updatedTransaction.ToTransactionModel<TransactionDto>();
                }
            }

            return null;
            
        }
    }
}