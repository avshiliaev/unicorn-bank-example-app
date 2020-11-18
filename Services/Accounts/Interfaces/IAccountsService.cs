using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Persistence.Models;

namespace Accounts.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountModel> CreateAccountAsync(AccountModel accountModel);
        
        Task<List<AccountModel>> ListAccountsAsync();

        Task<AccountModel> GetAccountByIdAsync(Guid accountId);

        Task<AccountModel> UpdateAccountAsync(AccountModel accountModel);
    }
}