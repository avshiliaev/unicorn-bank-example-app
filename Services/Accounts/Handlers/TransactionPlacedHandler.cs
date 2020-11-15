using Accounts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Accounts.Handlers
{
    public class TransactionPlacedHandler : ITransactionPlacedHandler
    {
        private IAccountsManager _accountsManager;
        private ILogger<AccountApprovedHandler> _logger;
        private IMessageBusService _messageBusService;

        public TransactionPlacedHandler(
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