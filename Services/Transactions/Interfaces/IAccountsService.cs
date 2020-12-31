using System;
using System.Threading.Tasks;
using Transactions.Persistence.Entities;

namespace Transactions.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountEntity?> CreateAccountAsync(AccountEntity accountEntity);

        Task<AccountEntity?> GetAccountByIdAsync(Guid accountId);

        Task<AccountEntity?> UpdateAccountAsync(AccountEntity accountEntity);
    }
}