using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Communication.Interfaces;
using Accounts.Interfaces;
using Accounts.Mappers;
using Microsoft.Extensions.Logging;
using Sdk.Api.ViewModels;

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

        public async Task<AccountEventViewModel> CreateNewAccountAsync(AccountEventViewModel accountEvent)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountEvent.ToNewAccountModel());
            // TODO: move topics definitions to sdk
            await _messageBusPublishService.PublishEventAsync(newAccount.ToAccountEvent());
            return newAccount.ToAccountEvent();
        }

        public async Task<List<AccountEventViewModel>> ListAccountsAsync()
        {
            var accounts = await _accountsService.ListAccountsAsync();
            return accounts.Select(acc => acc.ToAccountEvent()).ToList();
        }

        public async Task<AccountEventViewModel> UpdateExistingAccountAsync(AccountEventViewModel accountEvent)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountEvent.ToAccountModel());
            await _messageBusPublishService.PublishEventAsync(updatedAccount.ToAccountEvent());
            return updatedAccount.ToAccountEvent();
        }
    }
}