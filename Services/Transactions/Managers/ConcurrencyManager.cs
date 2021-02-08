using System.Threading.Tasks;
using Sdk.Interfaces;
using Transactions.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Managers
{
    public class ConcurrencyManager : IConcurrencyManager
    {
        private readonly IEventStoreService<TransactionEntity> _eventStoreService;

        public ConcurrencyManager(IEventStoreService<TransactionEntity> eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<ITransactionModel> SetNextSequentialNumber(ITransactionModel transactionModel)
        {
            var allTransactions = await _eventStoreService.GetAllEntitiesLastStatesAsync(
                entity => entity!.ProfileId == transactionModel.ProfileId
                          && entity!.AccountId == transactionModel.AccountId
            );
            var lastTransactionNumber = allTransactions.Max(t => t?.SequentialNumber);
            transactionModel.SequentialNumber = lastTransactionNumber.GetValueOrDefault(0) + 1;

            return transactionModel;
        }
    }
}