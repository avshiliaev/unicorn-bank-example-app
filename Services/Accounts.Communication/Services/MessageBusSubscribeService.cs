using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;

namespace Accounts.Communication.Services
{
    public class MessageBusSubscribeService : IConsumer<IMessage>
    {
        private readonly ILogger<MessageBusSubscribeService> _logger;

        public MessageBusSubscribeService(ILogger<MessageBusSubscribeService> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IMessage> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message);
        }
    }
}