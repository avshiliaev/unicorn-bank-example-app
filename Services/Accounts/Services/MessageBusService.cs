using System.Threading.Tasks;
using Accounts.Interfaces;
using Sdk.Api.Interfaces;

namespace Accounts.Services
{
    public class MessageBusService : IMessageBusService
    {
        public Task<bool> PublishEventAsync<T>(T eventToPublish) where T : IMessage
        {
            return Task.FromResult(true);
        }

        public Task<T> SubscribeToEventsAsync<T>(T eventToPublish) where T : IMessage
        {
            throw new System.NotImplementedException();
        }
    }
}