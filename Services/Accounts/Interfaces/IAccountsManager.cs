using System.Threading.Tasks;
using Accounts.Persistence.Models;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountModel> CreateNewAccountAsync(AccountModel accountModel);
        Task<AccountModel> UpdateExistingAccountAsync(AccountModel accountModel);
    }
}