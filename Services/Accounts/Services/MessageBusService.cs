using System.Threading.Tasks;
using Accounts.Interfaces;
using Google.Protobuf;

namespace Accounts.Services
{
    public class MessageBusService : IMessageBusService
    {
        public Task PublishEventAsync<T>(T eventToPublish, string topic) where T : IMessage
        {
            throw new System.NotImplementedException();
        }

        public Task<T> SubscribeToEventsAsync<T>(T eventToPublish, string topic) where T : IMessage
        {
            throw new System.NotImplementedException();
        }
    }
}