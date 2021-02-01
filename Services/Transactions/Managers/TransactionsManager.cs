using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;
using Transactions.Interfaces;
using Transactions.Mappers;

namespace Transactions.Managers
{
    public class TransactionsManager : ITransactionsManager
    {
        private readonly IConcurrencyManager _concurrencyManager;
        private readonly ILicenseManager<ITransactionModel> _licenseManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITransactionsService _transactionsService;

        public TransactionsManager(
            ILogger<TransactionsManager> logger,
            ITransactionsService transactionsService,
            IPublishEndpoint publishEndpoint,
            IConcurrencyManager concurrencyManager,
            ILicenseManager<ITransactionModel> licenseManager
        )
        {
            _transactionsService = transactionsService;
            _publishEndpoint = publishEndpoint;
            _concurrencyManager = concurrencyManager;
            _licenseManager = licenseManager;
        }

        public async Task<TransactionDto?> CreateNewTransactionAsync(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                transactionModel.AccountId == Guid.Empty.ToString() ||
                !await _licenseManager.EvaluatePendingAsync(transactionModel)
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
                await _publishEndpoint.Publish(newTransaction.ToTransactionModel<TransactionCheckCommand>());
                return newTransaction.ToTransactionModel<TransactionDto>();
            }

            return null;
        }

        public async Task<TransactionDto?> ProcessTransactionCheckedEventAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = await _transactionsService.GetTransactionByIdAsync(transactionModel.Id.ToGuid());
            if (transactionEntity != null)
            {
                if (transactionModel.IsBlocked())
                {
                    transactionEntity.SetBlocked();
                }
                else
                {
                    if (transactionModel.IsApproved())
                        transactionEntity.SetApproval();
                    else
                        transactionEntity.SetDenial();
                }

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