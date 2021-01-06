using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Moq;
using Sdk.Persistence.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class MongoChangeStreamMock<TEntity> : IEnumerator<ChangeStreamDocument<TEntity>>
        where TEntity : class, IMongoEntity
    {
        private List<TEntity> _entities;

        public MongoChangeStreamMock(List<TEntity> entities)
        {
            _entities = entities;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public ChangeStreamDocument<TEntity> Current { get; }

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class MongoRepositoryMockFactory<TEntity> : IMockFactory<IMongoRepository<TEntity>>
        where TEntity : class, IMongoEntity
    {
        private List<TEntity> _entities;

        public MongoRepositoryMockFactory(List<TEntity> entities)
        {
            _entities = entities;
        }

        public Mock<IMongoRepository<TEntity>> GetInstance()
        {
            var repository = new Mock<IMongoRepository<TEntity>>();
            repository
                .Setup(a => a.GetAll(It.IsAny<string>()))
                .Returns((string profileId) => _entities.Where(e => e.ProfileId == profileId).ToList());
            repository
                .Setup(a => a.Get(It.IsAny<string>()))
                .Returns((string id) => _entities.FirstOrDefault(e => e.Id == id));
            repository
                .Setup(a => a.SubscribeToChangesStreamMany(It.IsAny<string>()))
                .Returns((string id) => new MongoChangeStreamMock<TEntity>(_entities));
            repository
                .Setup(a => a.Create(It.IsAny<TEntity>()))
                .Returns((TEntity entity) =>
                {
                    entity.Id = Guid.NewGuid().ToString();
                    _entities.Add(entity);
                    return _entities.FirstOrDefault(a => a.Id == entity.Id);
                });
            repository
                .Setup(a => a.UpdatePassively(It.IsAny<string>(), It.IsAny<TEntity>()))
                .Returns((string id, TEntity entity) =>
                {
                    if (
                        entity == null ||
                        Guid.Empty.ToString() == id
                    )
                        return null;
                    _entities = _entities.Where(e => e.Id != id).ToList();
                    _entities.Add(entity);
                    return _entities.FirstOrDefault(e => e.Id == entity.Id);
                });
            return repository;
        }
    }
}