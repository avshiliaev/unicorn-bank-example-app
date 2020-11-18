using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using Microsoft.Extensions.Logging;
using Sdk.Api.ViewModels;

namespace Accounts.Managers
{
    public class AccountsManager : IAccountsManager
    {
        private readonly IAccountsService _accountsService;
        private readonly IMessageBusService _messageBusService;

        public AccountsManager(
            ILogger<AccountsManager> logger,
            IAccountsService accountsService,
            IMessageBusService messageBusService
        )
        {
            _accountsService = accountsService;
            _messageBusService = messageBusService;
        }

        public async Task<AccountEventViewModel> CreateNewAccountAsync(AccountEventViewModel accountEvent)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountEvent.ToNewAccountModel());
            // TODO: move topics definitions to sdk
            await _messageBusService.PublishEventAsync(newAccount.ToAccountEvent());
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
            await _messageBusService.PublishEventAsync(updatedAccount.ToAccountEvent());
            return updatedAccount.ToAccountEvent();
        }
    }
}