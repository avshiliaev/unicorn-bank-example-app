using System.Threading.Tasks;
using MassTransit;
using Sdk.Interfaces;

namespace Accounts.Communication.Interfaces
{
    public abstract class AMessageBusSubscribeService : IConsumer<IDataModel>
    {
        public virtual Task Consume(ConsumeContext<IDataModel> context)
        {
            throw new System.NotImplementedException();
        }
    }
}