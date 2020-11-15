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
            await _messageBusService.PublishEventAsync(newAccount.ToAccountEvent());
            return newAccount;
        }

        public Task<AccountModel> UpdateExistingAccountAsync(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }
    }
}