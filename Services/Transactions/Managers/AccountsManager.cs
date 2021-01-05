using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Interfaces;
using Transactions.Mappers;

namespace Transactions.Managers
{
    public class AccountsManager: IAccountsManager
    {
        private readonly IAccountsService _accountsService;

        public AccountsManager(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        
        public async Task<IAccountModel?> ProcessTransactionCheckedEventAsync(IAccountModel accountModel)
        {
            var existingAccount = await _accountsService.GetAccountByIdAsync(accountModel.Id.ToGuid());
            if (existingAccount != null)
            {
                if (accountModel.IsBlocked())
                    existingAccount.SetBlocked();
                var updatedAccount = await _accountsService.UpdateAccountAsync(existingAccount);
                return updatedAccount?.ToAccountModel<AccountDto>();
            }
            else
            {
                var createdAccount = await _accountsService.CreateAccountAsync(accountModel.ToAccountEntity());
                return createdAccount?.ToAccountModel<AccountDto>();
            }
        }
    }
}