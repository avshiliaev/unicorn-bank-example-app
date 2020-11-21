using System.Threading.Tasks;
using Accounts.Communication.Interfaces;
using MassTransit;
using Sdk.Interfaces;

namespace Accounts.Communication.Services
{
    public class MessageBusPublishService : IMessageBusPublishService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageBusPublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        
        public async Task<bool> PublishEventAsync<T>(T eventToPublish) where T : IDataModel
        {
            await _publishEndpoint.Publish<IDataModel>(new
            {
                Value = eventToPublish
            });
            return true;
        }
    }
}