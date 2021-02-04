using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sdk.Persistence.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class RepositoryMockFactory<TEntity> : IMockFactory<IRepository<TEntity>>
        where TEntity : class, IEventRecord
    {
        private List<TEntity> _entities;

        public RepositoryMockFactory(List<TEntity> entities)
        {
            _entities = entities;
        }

        public Mock<IRepository<TEntity>> GetInstance()
        {
            var repository = new Mock<IRepository<TEntity>>();
            repository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(
                    (Guid id) => Task.FromResult(
                        _entities.FirstOrDefault(a => a.Id == id)
                    )
                );
            repository
                .Setup(a => a.AppendStateOfEntity(It.IsAny<TEntity>()))
                .Returns((TEntity entity) =>
                {
                    entity.Id = Guid.NewGuid();
                    _entities.Add(entity);
                    return Task.FromResult(
                        _entities.FirstOrDefault(a => a.Id == entity.Id)
                    );
                });
            repository
                .Setup(a => a.UpdateAsync(It.IsAny<TEntity>()))
                .Returns((TEntity entity) =>
                {
                    if (
                        entity == null ||
                        Guid.Empty.Equals(entity.Id) ||
                        _entities
                            .FirstOrDefault(
                                e => e.Id.Equals(entity.Id) &&
                                     e.Version.Equals(entity.Version - 1)
                            ) == null
                    )
                        return Task.FromResult<TEntity>(null);

                    _entities = _entities.Where(e => e.Id != entity.Id).ToList();
                    _entities.Add(entity);
                    return Task.FromResult(
                        _entities.FirstOrDefault(e => e.Id == entity.Id)
                    );
                });
            repository
                .Setup(a => a.UpdateOptimisticallyAsync(It.IsAny<TEntity>()))
                .Returns((TEntity entity) =>
                {
                    if (
                        entity == null ||
                        Guid.Empty.Equals(entity.Id) ||
                        _entities
                            .FirstOrDefault(
                                e => e.Id.Equals(entity.Id) &&
                                     e.Version.Equals(entity.Version)
                            ) == null
                    )
                        return Task.FromResult<TEntity>(null);

                    _entities = _entities.Where(e => e.Id != entity.Id).ToList();
                    entity.Version = entity.Version + 1;
                    _entities.Add(entity);
                    return Task.FromResult(
                        _entities.FirstOrDefault(e => e.Id == entity.Id)
                    );
                });
            return repository;
        }
    }
}