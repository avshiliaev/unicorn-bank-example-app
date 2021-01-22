using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Notifications.Persistence.Entities;

namespace Notifications.Interfaces
{
    public interface INotificationsService
    {
        List<NotificationEntity> GetAll(string profileId);
        List<NotificationEntity> GetManyByParameter(Expression<Func<NotificationEntity, bool>> predicate, int count);
        NotificationEntity Get(string id);
        NotificationEntity Create(NotificationEntity entity);
        IEnumerator<ChangeStreamDocument<NotificationEntity>> SubscribeToChangesMany(
            PipelineDefinition<ChangeStreamDocument<
                NotificationEntity>, ChangeStreamDocument<NotificationEntity>
            > pipeline
        );
    }
}