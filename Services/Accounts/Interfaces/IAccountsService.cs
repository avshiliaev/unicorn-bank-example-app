using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;

namespace Accounts.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountEntity> CreateAccountAsync(AccountEntity accountModel);
        
        Task<List<AccountEntity>> ListAccountsAsync();

        Task<AccountEntity> GetAccountByIdAsync(Guid accountId);

        Task<AccountEntity> UpdateAccountAsync(AccountEntity accountModel);
    }
}