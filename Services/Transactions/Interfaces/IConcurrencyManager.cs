using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Transactions.Interfaces
{
    public interface IConcurrencyManager
    {
        public Task<ITransactionModel> SetNextSequentialNumber(ITransactionModel transactionModel);
    }
}