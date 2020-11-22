using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Dto;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountDto> CreateNewAccountAsync(AccountDto accountEvent);
        Task<AccountDto> UpdateExistingAccountAsync(AccountDto accountEvent);
    }
}