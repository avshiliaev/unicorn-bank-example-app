using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Accounts.Handlers
{
    public class AccountApprovedHandler : IConsumer<AccountDto>
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

        public async Task Consume(ConsumeContext<AccountDto> context)
        {
            _logger.LogDebug($"-------> AccountApprovedHandler {context}");
        }
    }
}