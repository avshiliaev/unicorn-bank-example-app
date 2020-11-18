using System.Collections.Generic;
using System.Threading.Tasks;
using Sdk.Api.ViewModels;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountEventViewModel> CreateNewAccountAsync(AccountEventViewModel accountEvent);
        Task<List<AccountEventViewModel>> ListAccountsAsync();
        Task<AccountEventViewModel> UpdateExistingAccountAsync(AccountEventViewModel accountEvent);
    }
}