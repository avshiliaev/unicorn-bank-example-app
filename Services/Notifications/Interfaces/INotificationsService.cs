using System.Collections.Generic;
using Notifications.Persistence.Entities;

namespace Notifications.Interfaces
{
    public interface INotificationsService
    {
        List<NotificationEntity> GetAll();
        NotificationEntity Get(string id);
        NotificationEntity Create(NotificationEntity entity);
        void Update(string id, NotificationEntity entityIn);
        void Remove(NotificationEntity entityIn);
        void Remove(string id);
        void SubscribeToChanges(string profileId);
    }
}