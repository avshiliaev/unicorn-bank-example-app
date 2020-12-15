using System.Collections.Generic;
using MongoDB.Driver;
using Notifications.Persistence.Entities;

namespace Notifications.Interfaces
{
    public interface INotificationsService
    {
        List<NotificationEntity> GetAll();
        NotificationEntity Get(string id);
        NotificationEntity Create(NotificationEntity entity);
        IEnumerator<ChangeStreamDocument<NotificationEntity>> SubscribeToChanges(string profileId);
    }
}