using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Transactions.Interfaces
{
    public interface IAccountsManager
    {
        Task<IAccountModel?> ProcessTransactionCheckedEventAsync(IAccountModel accountModel);
    }
}