using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Notifications.Interfaces;
using Notifications.Managers;
using Notifications.Persistence.Entities;
using Notifications.Services;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Notifications.Tests.Managers
{
    public class NotificationsManagerTests
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

        private readonly INotificationsManager _manager;

        public NotificationsManagerTests()
        {
            var notificationsRepositoryMock = new MongoRepositoryMockFactory<NotificationEntity>(_notificationEntities)
                .GetInstance();

            _manager = new NotificationsManager(
                new Mock<ILogger<NotificationsManager>>().Object,
                new NotificationsService(notificationsRepositoryMock.Object)
            );
        }

        #region AddFromAccount

        [Fact]
        public void ShouldSuccessfullyCreateFromAccount()
        {
            var approvedAccountEvent = new AccountApprovedEvent
            {
                Id = 1.ToGuid().ToString(),
                Balance = 3,
                Approved = true
            };
            var newNotification = _manager.AddFromAccount(approvedAccountEvent);
            Assert.NotNull(newNotification);
        }

        [Fact]
        public void ShouldNotCreateFromInvalidAccount()
        {
            var approvedAccountEvent = new AccountApprovedEvent
            {
                Approved = true
            };
            var newNotification = _manager.AddFromAccount(approvedAccountEvent);
            Assert.Null(newNotification);
        }

        #endregion

        #region AddFromTransaction

        [Fact]
        public void ShouldSuccessfullyCreateFromTransaction()
        {
            var transactionProcessedEvent = new TransactionProcessedEvent
            {
                Id = 1.ToGuid().ToString(),
                Approved = true
            };
            var newNotification = _manager.AddFromTransaction(transactionProcessedEvent);
            Assert.NotNull(newNotification);
        }

        [Fact]
        public void ShouldNotCreateFromInvalidTransaction()
        {
            var transactionProcessedEvent = new TransactionProcessedEvent
            {
                
            };
            var newNotification = _manager.AddFromTransaction(transactionProcessedEvent);
            Assert.Null(newNotification);
        }


        #endregion
    }
}