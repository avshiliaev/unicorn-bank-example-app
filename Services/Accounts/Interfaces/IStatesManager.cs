using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Accounts.Interfaces
{
    public interface IStatesManager
    {
        public Task<IAccountModel> ProcessAccountState(IAccountModel accountModel);
        public Task<ITransactionModel> ProcessTransactionState(ITransactionModel transactionModel);
    }
}