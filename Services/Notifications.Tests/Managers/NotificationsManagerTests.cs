using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Notifications.Interfaces;
using Notifications.Managers;
using Notifications.Persistence.Entities;
using Notifications.Services;
using Sdk.Api.Events;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Notifications.Tests.Managers
{
    public class NotificationsManagerTests
    {
        private readonly INotificationsManager _manager;

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

        public NotificationsManagerTests()
        {
            var notificationsRepositoryMock = new MongoRepositoryMockFactory<NotificationEntity>(_notificationEntities)
                .GetInstance();

            _manager = new NotificationsManager(
                new Mock<ILogger<NotificationsManager>>().Object,
                new NotificationsService(notificationsRepositoryMock.Object)
            );
        }

        [Fact]
        public void Should_AddNewNotification_Valid()
        {
            var notificationEvent = new NotificationEvent
            {
                Description = "Description",
                ProfileId = "awesome",
                Status = "approved",
                TimeStamp = DateTime.Now,
                Title = "Title",
                Id = Guid.NewGuid(),
                Version = 0
            };
            var newNotification = _manager.AddNewNotification(notificationEvent);
            Assert.NotNull(newNotification);
        }

        [Fact]
        public void ShouldNot_AddNewNotification_Invalid()
        {
            var notificationEvent = new NotificationEvent();
            var newNotification = _manager.AddNewNotification(notificationEvent);
            Assert.Null(newNotification);
        }
    }
}