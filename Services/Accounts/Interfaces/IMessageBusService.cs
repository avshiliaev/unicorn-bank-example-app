using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Accounts.Interfaces
{
    public interface IMessageBusService
    {
        Task PublishEventAsync<T>(T eventToPublish) where T : IMessage;
        Task<T> SubscribeToEventsAsync<T>(T eventToPublish) where T : IMessage;
    }
}