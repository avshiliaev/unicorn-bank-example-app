using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Accounts.Communication.Interfaces
{
    public interface IMessageBusPublishService
    {
        Task<bool> PublishEventAsync<T>(T eventToPublish) where T : IDataModel;
    }
}