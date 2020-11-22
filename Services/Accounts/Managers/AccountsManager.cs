using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Communication.Interfaces;
using Accounts.Dto;
using Accounts.Interfaces;
using Accounts.Mappers;
using Microsoft.Extensions.Logging;

namespace Accounts.Managers
{
    public class AccountsManager : IAccountsManager
    {
        private readonly IAccountsService _accountsService;
        private readonly IMessageBusPublishService _messageBusPublishService;

        public AccountsManager(
            ILogger<AccountsManager> logger,
            IAccountsService accountsService,
            IMessageBusPublishService messageBusPublishService
        )
        {
            _accountsService = accountsService;
            _messageBusPublishService = messageBusPublishService;
        }

        public async Task<AccountDto> CreateNewAccountAsync(AccountDto accountDto)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountDto.ToNewAccountEntity());
            var newAccountDto = newAccount.ToAccountDto();
            await _messageBusPublishService.PublishEventAsync(newAccountDto);
            
            return newAccountDto;
        }

        public async Task<AccountDto> UpdateExistingAccountAsync(AccountDto accountEvent)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountEvent.ToAccountEntity());
            var updatedAccountDto = updatedAccount.ToAccountDto();
            await _messageBusPublishService.PublishEventAsync(updatedAccountDto);
            
            return updatedAccountDto;
        }
    }
}