using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Managers
{
    /**
     * public async Task
     * <TransactionDto
     * ?>
     * CreateNewTransactionAsync(ITransactionModel transactionModel)
     * {
     * if (
     * string.IsNullOrEmpty(transactionModel.ProfileId) ||
     * transactionModel.AccountId == Guid.Empty.ToString() ||
     * !await _licenseManager.EvaluatePendingAsync(transactionModel)
     * )
     * return null;
     * 
     * // Optimistic Concurrency Control: set the sequential number
     * var sequencedTransaction = await _concurrencyManager.SetNextSequentialNumber(transactionModel);
     * 
     * var newTransaction = await _transactionsService.CreateTransactionAsync(
     * sequencedTransaction.ToTransactionEntity()
     * );
     * 
     * if (newTransaction != null)
     * {
     * await _publishEndpoint.Publish(newTransaction.ToTransactionModel
     * <TransactionCreatedEvent>
     *     ());
     *     await _publishEndpoint.Publish(newTransaction.ToTransactionModel
     *     <TransactionCheckCommand>
     *         ());
     *         return newTransaction.ToTransactionModel
     *         <TransactionDto>
     *             ();
     *             }
     *             return null;
     *             }
     *             public async Task<TransactionDto?> ProcessTransactionCheckedEventAsync(ITransactionModel transactionModel)
     *             {
     *             var transactionEntity = await _transactionsService.GetTransactionByIdAsync(transactionModel.Id.ToGuid());
     *             if (transactionEntity != null)
     *             {
     *             if (transactionModel.IsBlocked())
     *             {
     *             transactionEntity.SetBlocked();
     *             }
     *             else
     *             {
     *             if (transactionModel.IsApproved())
     *             transactionEntity.SetApproved();
     *             else
     *             transactionEntity.SetDenied();
     *             }
     *             // Optimistic Concurrency Control: increment the version on update
     *             var updatedTransaction = await _transactionsService.UpdateTransactionAsync(transactionEntity);
     *             if (updatedTransaction != null)
     *             {
     *             await _publishEndpoint.Publish(
     *             updatedTransaction.ToTransactionModel
     *             <TransactionUpdatedEvent>
     *                 ()
     *                 );
     *                 await _publishEndpoint.Publish(updatedTransaction.ToNotificationEvent());
     *                 return updatedTransaction.ToTransactionModel
     *                 <TransactionDto>
     *                     ();
     *                     }
     *                     }
     *                     return null;
     *                     }
     *                     *
     */
    public class EventStoreManager : IEventStoreManager<ATransactionsState>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private IEventStoreService<TransactionEntity> _eventStoreService;

        public EventStoreManager(
            IEventStoreService<TransactionEntity> eventStoreService,
            IPublishEndpoint publishEndpoint
        )
        {
            _eventStoreService = eventStoreService;
            _publishEndpoint = publishEndpoint;
        }

        public Task SaveStateOptimisticallyAsync(ATransactionsState dataModel)
        {
            throw new NotImplementedException();
        }

        public Task<ATransactionsState> SaveStateAsync(ATransactionsState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}