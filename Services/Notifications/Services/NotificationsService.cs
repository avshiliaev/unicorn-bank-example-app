using System.Collections.Generic;
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

        public List<NotificationEntity> GetAll()
        {
            return _mongoRepository.GetAll();
        }

        public NotificationEntity Get(string id)
        {
            return _mongoRepository.Get(id);
        }

        public NotificationEntity Create(NotificationEntity entity)
        {
            return _mongoRepository.Create(entity);
        }

        public void Update(string id, NotificationEntity entityIn)
        {
            _mongoRepository.Update(id, entityIn);
        }

        public void Remove(NotificationEntity entityIn)
        {
            _mongoRepository.Remove(entityIn);
        }

        public void Remove(string id)
        {
            _mongoRepository.Remove(id);
        }
    }
}