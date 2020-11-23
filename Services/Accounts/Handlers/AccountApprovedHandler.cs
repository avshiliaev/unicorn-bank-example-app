using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;

namespace Accounts.Handlers
{
    public class AccountApprovedHandler : IConsumer<IAccountModel>
    {
        private IAccountsManager _accountsManager;
        private readonly ILogger<AccountApprovedHandler> _logger;

        // TODO: Register handlers as subscribers!
        public AccountApprovedHandler(
            ILogger<AccountApprovedHandler> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public async Task Consume(ConsumeContext<IAccountModel> context)
        {
            _logger.LogDebug($"-------> AccountApprovedHandler {context.Message}");
        }
    }
}