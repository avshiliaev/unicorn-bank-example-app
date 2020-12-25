using System;
using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Accounts.Interfaces
{
    public interface IAccountsManager
    {
        Task<AccountDto?> CreateNewAccountAsync(string profileId);
        Task<AccountDto?> AddApprovalToAccountAsync(IAccountModel accountEvent);
        Task<AccountDto?> AddTransactionToAccountAsync(ITransactionModel transactionModel);
    }
}