using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
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

        public async Task<AccountDto> CreateNewAccountAsync(Guid profileId)
        {
            if (profileId != Guid.Empty)
            {
                var accountModel = new AccountDto {ProfileId = profileId.ToString()};
                var newAccount = await _accountsService.CreateAccountAsync(accountModel.ToAccountEntity());
                await _publishEndpoint.Publish(newAccount.ToAccountEvent<AccountCreatedEvent>());
                return newAccount.ToAccountModel<AccountDto>();
            }

            return null;
        }

        public async Task<AccountDto> UpdateExistingAccountAsync(IAccountModel accountModel)
        {
            var accountEntity = accountModel.ToAccountEntity();
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountEntity);
            if (updatedAccount != null)
            {
                await _publishEndpoint.Publish(updatedAccount.ToAccountEvent<AccountUpdatedEvent>());
                return updatedAccount.ToAccountModel<AccountDto>();
            }

            return null;
        }

        public async Task<AccountDto> AddTransactionToAccountAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = transactionModel.ToTransactionEntity();
            var mappedAccount = await _accountsService.GetAccountByIdAsync(transactionEntity.AccountId);
            if (mappedAccount != null)
            {
                mappedAccount.Balance += transactionEntity.Amount;
                var updatedAccount = await _accountsService.UpdateAccountAsync(mappedAccount);
                await _publishEndpoint.Publish(updatedAccount.ToAccountEvent<AccountUpdatedEvent>());
                return updatedAccount.ToAccountModel<AccountDto>();
            }

            return null;
        }
    }
}