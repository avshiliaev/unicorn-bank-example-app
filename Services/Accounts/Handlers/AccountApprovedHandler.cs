using System.Threading.Tasks;
using Accounts.Communication.Interfaces;
using Accounts.Dto;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Interfaces;

namespace Accounts.Handlers
{
    public class AccountApprovedHandler : AMessageBusSubscribeService
    {
        private IAccountsManager _accountsManager;
        private ILogger<AccountApprovedHandler> _logger;
        private IMessageBusPublishService _messageBusPublishService;
        
        // TODO: Register handlers as subscribers!
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

        public override async Task Consume(ConsumeContext<IDataModel> context)
        {
            _logger.LogDebug($"-------> AccountApprovedHandler {context}");
        }
    }
}