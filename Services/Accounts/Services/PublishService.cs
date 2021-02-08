using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.Services
{
    public class PublishService : IPublishService<AAccountState>
    {
        // TODO MassTransit
        public Task<AAccountState> Publish(AAccountState entityState)
        {
            throw new System.NotImplementedException();
        }
    }
}