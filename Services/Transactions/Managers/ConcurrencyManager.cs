using System.Linq;
using System.Threading.Tasks;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Interfaces;

namespace Transactions.Managers
{
    public class ConcurrencyManager : IConcurrencyManager
    {
        private readonly ITransactionsService _transactionsService;

        public ConcurrencyManager(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }
        
        public async Task<ITransactionModel> SetNextSequentialNumber(ITransactionModel transactionModel)
        {
            
            var allTransactions = await _transactionsService.GetManyByParameterAsync(
                entity => entity!.ProfileId == transactionModel.ProfileId 
                          && entity!.AccountId == transactionModel.AccountId.ToGuid()
            );
            var lastTransactionNumber = allTransactions.Max(t => t?.SequentialNumber);
            transactionModel.SequentialNumber = lastTransactionNumber.GetValueOrDefault(0) + 1;
            
            return transactionModel;
        }
    }
}