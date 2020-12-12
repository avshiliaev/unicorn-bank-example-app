using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Moq;
using Sdk.Api.Interfaces;
using Sdk.Tests.Interfaces;

namespace Accounts.Tests.Mocks
{
    public class PublishEndpointMockFactory : IMockFactory<IPublishEndpoint>
    {
        public Mock<IPublishEndpoint> GetInstance()
        {
            var publishEndpoint = new Mock<IPublishEndpoint>();
            publishEndpoint
                .Setup(
                    p => p.Publish(
                        It.IsAny<IAccountModel>(), It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);
            return publishEndpoint;
        }
    }
}