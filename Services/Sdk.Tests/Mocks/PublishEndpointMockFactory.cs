using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Moq;
using Sdk.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class PublishEndpointMockFactory<TModel> : IMockFactory<IPublishEndpoint>
        where TModel : class, IDataModel
    {
        public Mock<IPublishEndpoint> GetInstance()
        {
            var publishEndpoint = new Mock<IPublishEndpoint>();
            publishEndpoint
                .Setup(
                    p => p.Publish(
                        It.IsAny<TModel>(), It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);
            return publishEndpoint;
        }
    }
}