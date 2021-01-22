using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Transactions.Persistence.Entities;

namespace Transactions.Interfaces
{
    public interface IAccountsService
    {
        Task<AccountEntity?> CreateAccountAsync(AccountEntity accountEntity);
        
        Task<AccountEntity?> GetOneByParameterAsync(Expression<Func<AccountEntity, bool>> predicate);

        Task<AccountEntity?> GetAccountByIdAsync(Guid accountId);

        Task<AccountEntity?> UpdateAccountAsync(AccountEntity accountEntity);
    }
}