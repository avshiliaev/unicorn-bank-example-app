using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Accounts.Handlers
{
    public class SubscriptionsHandler : IConsumer<AccountDto>, IConsumer<TransactionDto>
    {
        private readonly ILogger<SubscriptionsHandler> _logger;
        private IAccountsManager _accountsManager;

        // TODO: Register handlers as subscribers!
        public SubscriptionsHandler(
            ILogger<SubscriptionsHandler> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public SubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountDto> context)
        {
            _logger.LogDebug($"-------> AccountDto {context}");
        }

        public async Task Consume(ConsumeContext<TransactionDto> context)
        {
            _logger.LogDebug($"-------> TransactionDto {context}");
        }
    }
}