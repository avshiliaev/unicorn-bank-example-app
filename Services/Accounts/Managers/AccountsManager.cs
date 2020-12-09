using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;

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

        public async Task<AccountDto> CreateNewAccountAsync(AccountDto accountDto)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountDto.ToNewAccountEntity());
            await _publishEndpoint.Publish(newAccount.ToAccountCreatedEvent());
            return newAccount.ToAccountDto();
        }

        public async Task<AccountDto> UpdateExistingAccountAsync(AccountDto accountEvent)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountEvent.ToAccountEntity());
            await _publishEndpoint.Publish(updatedAccount.ToAccountUpdatedEvent());

            return updatedAccount.ToAccountDto();
        }
    }
}