using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Transactions.Interfaces
{
    public interface IConcurrencyManager
    {
        public Task<ITransactionModel> SetNextSequentialNumber(ITransactionModel transactionModel);
    }
}