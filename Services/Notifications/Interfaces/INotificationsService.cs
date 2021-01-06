using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Notifications.Persistence.Entities;

namespace Notifications.Interfaces
{
    public interface INotificationsService
    {
        List<NotificationEntity> GetAll(string profileId);
        List<NotificationEntity> GetManyByParameter(Expression<Func<NotificationEntity, bool>> predicate);
        NotificationEntity Get(string id);
        NotificationEntity Create(NotificationEntity entity);
        IEnumerator<ChangeStreamDocument<NotificationEntity>> SubscribeToChanges(string pipeline);
    }
}