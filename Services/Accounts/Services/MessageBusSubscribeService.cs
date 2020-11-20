using System.Threading.Tasks;
using Accounts.Communication.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;

namespace Accounts.Services
{
    public class MessageBusSubscribeService : AMessageBusSubscribeService
    {
        private readonly ILogger<MessageBusSubscribeService> _logger;

        public MessageBusSubscribeService(ILogger<MessageBusSubscribeService> logger)
        {
            _logger = logger;
        }

        public override async Task Consume(ConsumeContext<IMessage> context)
        {
            _logger.LogInformation("MY CUSTOM CONSUMER");
            _logger.LogInformation("Value: {Value}", context.Message);
        }
    }
}