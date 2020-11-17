using System.Threading.Tasks;
using Google.Protobuf;

namespace Accounts.Interfaces
{
    public interface IMessageBusService
    {
        Task PublishEventAsync<T>(T eventToPublish, string topic) where T : IMessage;
        Task<T> SubscribeToEventsAsync<T>(T eventToPublish, string topic) where T : IMessage;
    }
}