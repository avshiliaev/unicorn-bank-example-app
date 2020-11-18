using System.Threading.Tasks;
using Accounts.Interfaces;
using Sdk.Api.Interfaces;

namespace Accounts.Services
{
    public class MessageBusService : IMessageBusService
    {
        public Task PublishEventAsync<T>(T eventToPublish) where T : IMessage
        {
            throw new System.NotImplementedException();
        }

        public Task<T> SubscribeToEventsAsync<T>(T eventToPublish) where T : IMessage
        {
            throw new System.NotImplementedException();
        }
    }
}