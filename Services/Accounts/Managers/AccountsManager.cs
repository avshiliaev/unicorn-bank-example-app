using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
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
                var accountRequest = new AccountDto {ProfileId = profileId};
                accountRequest.SetPending();

                var newAccount = await _accountsService.CreateAccountAsync(accountRequest.ToAccountEntity());
                
                await _publishEndpoint.Publish(newAccount?.ToAccountEvent<AccountCreatedEvent>());
                await _publishEndpoint.Publish(newAccount?.ToAccountEvent<AccountCheckCommand>());
                
                return newAccount?.ToAccountModel<AccountDto>();
            }

            return null;
        }

        public async Task<AccountDto?> ProcessAccountIsCheckedEventAsync(IAccountModel accountModel)
        {
            var accountEntity = await _accountsService.GetAccountByIdAsync(accountModel.Id.ToGuid());
            if (accountEntity != null)
            {
                if (accountModel.IsBlocked())
                    accountEntity.SetBlocked();
                else
                {
                    if (accountModel.IsApproved())
                        accountEntity.SetApproval();
                    else
                        accountEntity.SetDenial();
                }

                // Optimistic Concurrency Control: update incrementing the version
                var updatedAccount = await _accountsService.UpdateAccountAsync(accountEntity);
                if (updatedAccount != null)
                {
                    await _publishEndpoint.Publish(updatedAccount.ToAccountEvent<AccountUpdatedEvent>());
                    await _publishEndpoint.Publish(updatedAccount.ToNotificationEvent());
                    
                    return updatedAccount.ToAccountModel<AccountDto>();
                }
            }

            return null;
        }

        public async Task<AccountDto?> ProcessTransactionUpdatedEventAsync(ITransactionModel transactionModel)
        {
            var transactionEntity = transactionModel.ToTransactionEntity();
            var mappedAccount = await _accountsService.GetAccountByIdAsync(transactionEntity.AccountId);
            if (mappedAccount != null)
            {
                // Optimistic Concurrency Control: check version
                if (!mappedAccount.CheckConcurrentController(transactionEntity))
                    return null;

                mappedAccount.SetBalance(transactionEntity);
                mappedAccount.IncrementConcurrentController();

                // Optimistic Concurrency Control: update incrementing the version
                var updatedAccount = await _accountsService.UpdateAccountAsync(mappedAccount);
                
                await _publishEndpoint.Publish(updatedAccount?.ToAccountEvent<AccountUpdatedEvent>());
                await _publishEndpoint.Publish(updatedAccount?.ToAccountEvent<AccountCheckCommand>());
                
                return updatedAccount?.ToAccountModel<AccountDto>();
            }

            return null;
        }
    }
}