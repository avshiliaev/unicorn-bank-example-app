using Accounts.Communication.Interfaces;
using Accounts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Accounts.Handlers
{
    public class AccountApprovedHandler : IAccountApprovedHandler
    {
        private IAccountsManager _accountsManager;
        private ILogger<AccountApprovedHandler> _logger;
        private IMessageBusPublishService _messageBusPublishService;

        public AccountApprovedHandler(
            ILogger<AccountApprovedHandler> logger,
            IAccountsManager accountsManager,
            IMessageBusPublishService messageBusPublishService
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
            _messageBusPublishService = messageBusPublishService;
        }
    }
}