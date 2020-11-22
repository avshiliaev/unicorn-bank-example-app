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
            var newAccountDto = newAccount.ToAccountDto();
            await _publishEndpoint.Publish(newAccountDto);

            return newAccountDto;
        }

        public async Task<AccountDto> UpdateExistingAccountAsync(AccountDto accountEvent)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountEvent.ToAccountEntity());
            var updatedAccountDto = updatedAccount.ToAccountDto();
            await _publishEndpoint.Publish(updatedAccountDto);

            return updatedAccountDto;
        }
    }
}