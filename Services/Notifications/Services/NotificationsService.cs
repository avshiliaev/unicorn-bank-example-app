using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Notifications.Interfaces;
using Notifications.Persistence.Entities;
using Sdk.Persistence.Extensions;
using Sdk.Persistence.Interfaces;

namespace Notifications.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IMongoRepository<NotificationEntity> _mongoRepository;

        public NotificationsService(IMongoRepository<NotificationEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public List<NotificationEntity> GetAll(string profileId)
        {
            return _mongoRepository.GetAll(profileId)!;
        }

        public List<NotificationEntity> GetManyByParameter(Expression<Func<NotificationEntity, bool>> predicate)
        {
            return _mongoRepository.GetManyByParameter(predicate)!;
        }

        
        public NotificationEntity Get(string id)
        {
            return _mongoRepository.Get(id);
        }

        public NotificationEntity Create(NotificationEntity entity)
        {
            return _mongoRepository.Create(entity);
        }

        public IEnumerator<ChangeStreamDocument<NotificationEntity>> SubscribeToChanges(string pipeline)
        {
            return _mongoRepository.SubscribeToChangesStreamMany(pipeline)!;
        }
    }
}