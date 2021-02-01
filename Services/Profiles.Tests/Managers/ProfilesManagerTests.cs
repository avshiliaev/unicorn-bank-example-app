using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Profiles.Interfaces;
using Profiles.Managers;
using Profiles.Persistence.Entities;
using Profiles.Services;
using Sdk.Api.Events;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Profiles.Tests.Managers
{
    public class ProfilesManagerTests
    {
        private readonly IProfilesManager _manager;

        private readonly List<ProfileEntity> _notificationEntities = new List<ProfileEntity>
        {
            new ProfileEntity
            {
                Id = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new ProfileEntity
            {
                Id = 2.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new ProfileEntity
            {
                Id = 3.ToGuid().ToString(),
                ProfileId = 2.ToGuid().ToString(),
                Version = 0
            }
        };

        public ProfilesManagerTests()
        {
            var notificationsRepositoryMock = new MongoRepositoryMockFactory<ProfileEntity>(_notificationEntities)
                .GetInstance();

            _manager = new ProfilesManager(
                new Mock<ILogger<ProfilesManager>>().Object,
                new ProfilesService(notificationsRepositoryMock.Object)
            );
        }

        [Fact]
        public void Should_AddNewProfile_Valid()
        {
            var notificationEvent = new AccountCreatedEvent
            {
                ProfileId = "awesome",
                Id = Guid.NewGuid().ToString(),
                Version = 0
            };
            var newProfile = _manager.AddNewProfile(notificationEvent);
            Assert.NotNull(newProfile);
        }

        [Fact]
        public void ShouldNot_AddNewProfile_Invalid()
        {
            var notificationEvent = new AccountCreatedEvent();
            var newProfile = _manager.AddNewProfile(notificationEvent);
            Assert.Null(newProfile);
        }
    }
}