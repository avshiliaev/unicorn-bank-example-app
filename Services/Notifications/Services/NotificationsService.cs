using System;
using System.Collections.Generic;
using Notifications.Persistence.Entities;

namespace Notifications.Services
{
    public class NotificationsService : INotificationsService
    {
        public List<NotificationEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public NotificationEntity Get(string id)
        {
            throw new NotImplementedException();
        }

        public NotificationEntity Create(NotificationEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, NotificationEntity entityIn)
        {
            throw new NotImplementedException();
        }

        public void Remove(NotificationEntity entityIn)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }

    public interface INotificationsService
    {
        List<NotificationEntity> GetAll();
        NotificationEntity Get(string id);
        NotificationEntity Create(NotificationEntity entity);
        void Update(string id, NotificationEntity entityIn);
        void Remove(NotificationEntity entityIn);
        void Remove(string id);
    }
}