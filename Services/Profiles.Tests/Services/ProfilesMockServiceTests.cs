using System;
using System.Collections.Generic;
using Profiles.Interfaces;
using Profiles.Persistence.Entities;
using Profiles.Services;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Profiles.Tests.Services
{
    public class ProfilesServiceTests
    {
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

        private readonly IProfilesService _service;

        public ProfilesServiceTests()
        {
            var notificationsRepositoryMock = new MongoRepositoryMockFactory<ProfileEntity>(_notificationEntities)
                .GetInstance();
            _service = new ProfilesService(notificationsRepositoryMock.Object);
        }

        [Fact]
        public void Should_Create_Valid()
        {
            var newProfileEntity = new ProfileEntity
            {
                EntityId = Guid.NewGuid().ToString(),
                ProfileId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = 0
            };
            var newCreatedAccountEntity = _service.Create(newProfileEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }

        [Fact]
        public void ShouldNot_Create_Invalid()
        {
            var invalidProfileEntity = new ProfileEntity();
            var newCreatedAccountEntity = _service.Create(invalidProfileEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }
    }
}