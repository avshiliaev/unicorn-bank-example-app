using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class RepositoryMockFactory<TEntity> : IMockFactory<IRepository<TEntity>>
        where TEntity : class, IRecord
    {
        private readonly List<TEntity> _entities;

        public RepositoryMockFactory(List<TEntity> entities)
        {
            _entities = entities;
        }

        public Mock<IRepository<TEntity>> GetInstance()
        {
            var repository = new Mock<IRepository<TEntity>>();
            repository
                .Setup(a => a.AppendStateOfEntity(It.IsAny<TEntity>()))
                .Returns(
                    (TEntity entity) => Task.FromResult(
                        _entities.FirstOrDefault(a => a.EntityId == entity.EntityId)
                    )
                );
            return repository;
        }
    }
}