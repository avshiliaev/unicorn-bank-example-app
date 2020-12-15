using System;
using System.Collections.Generic;
using System.Globalization;
using Notifications.Interfaces;
using Notifications.Persistence.Entities;
using Notifications.Services;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Notifications.Tests.Services
{
    public class NotificationsServiceTests
    {
        private readonly List<NotificationEntity> _notificationEntities = new List<NotificationEntity>
        {
            new NotificationEntity
            {
                Id = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new NotificationEntity
            {
                Id = 2.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new NotificationEntity
            {
                Id = 3.ToGuid().ToString(),
                ProfileId = 2.ToGuid().ToString(),
                Version = 0
            }
        };

        private readonly INotificationsService _service;

        public NotificationsServiceTests()
        {
            var notificationsRepositoryMock = new MongoRepositoryMockFactory<NotificationEntity>(_notificationEntities)
                .GetInstance();
            _service = new NotificationsService(notificationsRepositoryMock.Object);
        }

        [Fact]
        public void ShouldSuccessfullyCreateANewNotification()
        {
            var newNotificationEntity = new NotificationEntity
            {
                Description = "Test",
                ProfileId = Guid.NewGuid().ToString(),
                Status = "approved",
                TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Title = "Title",
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = 0
            };
            var newCreatedAccountEntity = _service.Create(newNotificationEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }

        [Fact]
        public void ShouldNotCreateAnInvalidNotification()
        {
            var invalidNotificationEntity = new NotificationEntity();
            var newCreatedAccountEntity = _service.Create(invalidNotificationEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }
    }
}