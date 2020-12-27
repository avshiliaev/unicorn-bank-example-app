using System.Collections.Generic;
using MongoDB.Driver;
using Notifications.Interfaces;
using Notifications.Persistence.Entities;
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

        public NotificationEntity Get(string id)
        {
            return _mongoRepository.Get(id);
        }

        public NotificationEntity Create(NotificationEntity entity)
        {
            return _mongoRepository.Create(entity);
        }

        public IEnumerator<ChangeStreamDocument<NotificationEntity>> SubscribeToChanges(string profileId)
        {
            return _mongoRepository.SubscribeToChangesStream(profileId)!;
        }
    }
}