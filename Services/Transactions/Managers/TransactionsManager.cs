using System;
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
                var newTransaction = await _transactionsService.CreateTransactionAsync(
                    transactionModel.ToTransactionEntity()
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
                var updatedTransaction = await _transactionsService.UpdateTransactionAsync(transactionEntity);
                if (updatedTransaction != null)
                {
                    await _publishEndpoint
                        .Publish(
                            updatedTransaction.ToTransactionModel<TransactionUpdatedEvent>()
                        );
                    await _publishEndpoint
                        .Publish(new NotificationEvent
                        {
                            Description = "Your transaction has been processed.",
                            ProfileId = updatedTransaction.ProfileId,
                            Status = "processed",
                            TimeStamp = DateTime.Now,
                            Title = "Transaction processed",
                            Id = Guid.NewGuid(),
                            Version = 0
                        });
                    return updatedTransaction.ToTransactionModel<TransactionDto>();
                }
            }

            return null;
        }
    }
}