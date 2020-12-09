using System.Threading;
using System.Threading.Tasks;
using Accounts.Tests.Interfaces;
using MassTransit;
using Moq;
using Sdk.Api.Interfaces;

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