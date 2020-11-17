using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using Accounts.Persistence.Models;
using Microsoft.Extensions.Logging;

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

        public async Task<AccountModel> CreateNewAccountAsync(AccountModel accountModel)
        {
            var newAccount = await _accountsService.CreateAccountAsync(accountModel);
            // TODO: move topics definitions to sdk
            await _messageBusService.PublishEventAsync(newAccount.ToAccountEvent(), "AccountCreated");
            return newAccount;
        }

        public async Task<AccountModel> UpdateExistingAccountAsync(AccountModel accountModel)
        {
            var updatedAccount = await _accountsService.UpdateAccountAsync(accountModel);
            await _messageBusService.PublishEventAsync(updatedAccount.ToAccountEvent(), "AccountUpdated");
            return updatedAccount;
        }
    }
}