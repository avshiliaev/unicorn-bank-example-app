using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Interfaces;
using Transactions.Mappers;

namespace Transactions.Managers
{
    public class TransactionsManager : ITransactionsManager
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITransactionsService _transactionsService;

        public TransactionsManager(
            ILogger<TransactionsManager> logger,
            ITransactionsService transactionsService,
            IPublishEndpoint publishEndpoint
        )
        {
            _transactionsService = transactionsService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TransactionDto?> CreateNewTransactionAsync(ITransactionModel transactionModel)
        {
            if (
                !string.IsNullOrEmpty(transactionModel.ProfileId) &&
                transactionModel.AccountId != Guid.Empty.ToString()
            )
            {
                // Optimistic Concurrency Control: set the sequential number
                var allTransactions = await _transactionsService.GetLastTransactionNumber(
                    entity => entity!.ProfileId == transactionModel.ProfileId
                );
                var lastTransactionNumber = allTransactions.Max(t => t?.SequentialNumber);

                var newTransaction = await _transactionsService.CreateTransactionAsync(
                    transactionModel.ToTransactionEntity(lastTransactionNumber.GetValueOrDefault(0) + 1)
                );
                await _publishEndpoint.Publish(newTransaction?.ToTransactionModel<TransactionCreatedEvent>());
                return newTransaction?.ToTransactionModel<TransactionDto>();
            }

            return null;
        }

        public async Task<TransactionDto?> UpdateStatusOfTransactionAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = await _transactionsService.GetTransactionByIdAsync(transactionModel.Id.ToGuid());
            if (transactionEntity != null)
            {
                transactionEntity.Approved = transactionModel.Approved;
                transactionEntity.Pending = false;

                // Optimistic Concurrency Control: increment the version on update
                var updatedTransaction = await _transactionsService.UpdateTransactionAsync(transactionEntity);
                if (updatedTransaction != null)
                {
                    await _publishEndpoint
                        .Publish(
                            updatedTransaction.ToTransactionModel<TransactionUpdatedEvent>()
                        );
                    await _publishEndpoint.Publish(new NotificationEvent
                    {
                        Description =
                            $"Your transaction has been {(transactionEntity.Approved ? "processed" : "declined")}.",
                        ProfileId = updatedTransaction.ProfileId,
                        Status = updatedTransaction.Approved ? "processed" : "declined",
                        TimeStamp = DateTime.Now,
                        Title = $"{(updatedTransaction.Approved ? "Processing" : "Denial")}",
                        Id = Guid.NewGuid()
                    });
                    return updatedTransaction.ToTransactionModel<TransactionDto>();
                }
            }

            return null;
        }
    }
}