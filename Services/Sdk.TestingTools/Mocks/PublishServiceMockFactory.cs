using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class PublishServiceMockFactory<TModel, TEvent> : IMockFactory<IPublishService<TModel>>
        where TModel : class, IEntityState
        where TEvent : class, IEvent
    {
        public Mock<IPublishService<TModel>> GetInstance()
        {
            var eMock = new Mock<IPublishService<TModel>>();
            eMock
                .Setup(
                    p => p.Publish<TEvent>(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(
                    (TModel model) => Task.FromResult(model)
                );

            return eMock;
        }
    }
}