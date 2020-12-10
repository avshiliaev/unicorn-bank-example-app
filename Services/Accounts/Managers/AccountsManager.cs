using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;

namespace Accounts.Managers
{
    public class AccountsManager : IAccountsManager
    {
        private readonly IAccountsService _accountsService;
        private readonly IPublishEndpoint _publishEndpoint;

        public AccountsManager(
            ILogger<AccountsManager> logger,
            IAccountsService accountsService,
            IPublishEndpoint publishEndpoint
        )
        {
            _accountsService = accountsService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<AccountDto> CreateNewAccountAsync(IAccountModel accountModel)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountModel.ToNewAccountEntity());
            await _publishEndpoint.Publish(newAccount.ToAccountCreatedEvent());
            return newAccount.ToAccountDto();
        }

        public async Task<AccountDto> UpdateExistingAccountAsync(IAccountModel accountModel)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountModel.ToAccountEntity());
            await _publishEndpoint.Publish(updatedAccount.ToAccountUpdatedEvent());

            return updatedAccount.ToAccountDto();
        }

        public async Task<AccountDto> AddTransactionToAccountAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = transactionModel.ToTransactionEntity();
            var mappedAccount = await _accountsService.GetAccountByIdAsync(transactionEntity.AccountId);
            
            mappedAccount.Balance += transactionEntity.Amount;
            
            var updatedAccount = await _accountsService.UpdateAccountAsync(mappedAccount);
            await _publishEndpoint.Publish(updatedAccount.ToAccountUpdatedEvent());

            return updatedAccount.ToAccountDto();
        }
    }
}