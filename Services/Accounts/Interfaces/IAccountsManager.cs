using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountDto?> CreateNewAccountAsync(string profileId);
        Task<AccountDto?> ProcessAccountIsCheckedEventAsync(IAccountModel accountEvent);
        Task<AccountDto?> ProcessTransactionUpdatedEventAsync(ITransactionModel transactionModel);
    }
}