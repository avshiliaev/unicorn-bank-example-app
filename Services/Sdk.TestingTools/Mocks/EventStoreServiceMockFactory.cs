using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class EventStoreServiceMockFactory<TModel> : IMockFactory<IEventStoreService<TModel>>
        where TModel : class, IEntityState, IDataModel
    {
        private readonly List<TModel> _models;

        public EventStoreServiceMockFactory(List<TModel> models)
        {
            _models = models;
        }

        public Mock<IEventStoreService<TModel>> GetInstance()
        {
            var repository = new Mock<IEventStoreService<TModel>>();
            repository
                .Setup(a => a.AppendStateOfEntity(It.IsAny<TModel>()))
                .Returns(
                    (TModel model) => Task.FromResult(
                        _models.FirstOrDefault(a => a.EntityId == model.EntityId)
                    )
                );
            return repository;
        }
    }
}