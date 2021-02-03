using System;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Managers
{
    public class EventStoreManager : IEventStoreManager<AAccountState>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private IEventStoreService<AccountEntity> _eventStoreService;

        public EventStoreManager(
            IEventStoreService<AccountEntity> eventStoreService,
            IPublishEndpoint publishEndpoint
        )
        {
            _eventStoreService = eventStoreService;
            _publishEndpoint = publishEndpoint;
        }

        /**
         * public async Task
         * <AccountDto
         * ?>
         * ProcessTransactionUpdatedEventAsync(ITransactionModel transactionModel)
         * {
         * if (!transactionModel.IsApproved())
         * return null;
         * var transactionEntity = transactionModel.ToTransactionEntity();
         * var mappedAccount = await _accountsService.GetAccountByIdAsync(transactionEntity.AccountId);
         * if (mappedAccount != null)
         * {
         * // Optimistic Concurrency Control: check version
         * if (!mappedAccount.CheckConcurrentController(transactionEntity))
         * return null;
         * 
         * mappedAccount.SetBalance(transactionEntity);
         * mappedAccount.IncrementConcurrentController();
         * 
         * // Optimistic Concurrency Control: update incrementing the version
         * var updatedAccount = await _accountsService.UpdateAccountAsync(mappedAccount);
         * 
         * await _publishEndpoint.Publish(updatedAccount?.ToAccountEvent
         * <AccountUpdatedEvent>
         *     ());
         *     await _publishEndpoint.Publish(updatedAccount?.ToAccountEvent
         *     <AccountCheckCommand>
         *         ());
         *         return updatedAccount?.ToAccountModel
         *         <AccountDto>
         *             ();
         *             }
         *             return null;
         *             }
         *             *
         */
        public Task<AAccountState> SaveStateAsync(AAccountState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}