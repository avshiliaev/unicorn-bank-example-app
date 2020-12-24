using System;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;

namespace Accounts.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountEntity?> CreateAccountAsync(AccountEntity accountEntity);

        Task<AccountEntity?> GetAccountByIdAsync(Guid accountId);

        Task<AccountEntity?> UpdateAccountAsync(AccountEntity accountEntity);
    }
}