using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

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

        public async Task<AccountDto?> CreateNewAccountAsync(string profileId)
        {
            if (!string.IsNullOrEmpty(profileId))
            {
                var accountModel = new AccountDto {ProfileId = profileId};
                var newAccount = await _accountsService.CreateAccountAsync(accountModel.ToAccountEntity());
                await _publishEndpoint.Publish(newAccount?.ToAccountEvent<AccountCreatedEvent>());
                return newAccount?.ToAccountModel<AccountDto>();
            }

            return null;
        }

        public async Task<AccountDto?> AddApprovalToAccountAsync(IAccountModel accountModel)
        {
            var accountEntity = await _accountsService.GetAccountByIdAsync(accountModel.Id.ToGuid());
            if (accountEntity != null)
            {
                accountEntity.Approved = accountModel.Approved;
                var updatedAccount = await _accountsService.UpdateAccountAsync(accountEntity);
                if (updatedAccount != null)
                {
                    await _publishEndpoint.Publish(updatedAccount.ToAccountEvent<AccountUpdatedEvent>());
                    await _publishEndpoint.Publish(new NotificationEvent
                    {
                        Description = "Your account has been approved.",
                        ProfileId = updatedAccount.ProfileId,
                        Status = "approved",
                        TimeStamp = DateTime.Now,
                        Title = "Account approved",
                        Id = Guid.NewGuid(),
                        Version = 0
                    });
                    return updatedAccount.ToAccountModel<AccountDto>();
                }
            }

            return null;
        }

        public async Task<AccountDto?> AddTransactionToAccountAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = transactionModel.ToTransactionEntity();
            var mappedAccount = await _accountsService.GetAccountByIdAsync(transactionEntity.AccountId);
            if (mappedAccount != null)
            {
                mappedAccount.Balance += transactionEntity.Amount;
                var updatedAccount = await _accountsService.UpdateAccountAsync(mappedAccount);
                await _publishEndpoint.Publish(updatedAccount?.ToAccountEvent<AccountUpdatedEvent>());
                return updatedAccount?.ToAccountModel<AccountDto>();
            }

            return null;
        }
    }
}