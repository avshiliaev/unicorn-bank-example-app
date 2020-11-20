using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Interfaces;

namespace Accounts.Communication.Interfaces
{
    public abstract class AMessageBusSubscribeService : IConsumer<IMessage>
    {
        public virtual Task Consume(ConsumeContext<IMessage> context)
        {
            throw new System.NotImplementedException();
        }
    }
}