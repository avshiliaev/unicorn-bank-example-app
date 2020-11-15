using Accounts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Accounts.Handlers
{
    public class AccountApprovedHandler : IAccountApprovedHandler
    {
        private IAccountsManager _accountsManager;
        private ILogger<AccountApprovedHandler> _logger;
        private IMessageBusService _messageBusService;

        public AccountApprovedHandler(
            ILogger<AccountApprovedHandler> logger,
            IAccountsManager accountsManager,
            IMessageBusService messageBusService
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
            _messageBusService = messageBusService;
        }
    }
}