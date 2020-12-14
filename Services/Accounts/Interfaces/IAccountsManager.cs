using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountDto> CreateNewAccountAsync(IAccountModel accountEvent);
        Task<AccountDto> UpdateExistingAccountAsync(IAccountModel accountEvent);
        Task<AccountDto> AddTransactionToAccountAsync(ITransactionModel transactionModel);
    }
}