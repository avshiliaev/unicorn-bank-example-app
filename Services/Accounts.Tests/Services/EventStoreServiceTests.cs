using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Mappers;
using Accounts.Persistence.Models;
using Accounts.Services;
using Accounts.States.Account;
using AutoMapper;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Accounts.Tests.Services
{
    public class EventStoreServiceTests
    {
        private readonly List<AccountRecord> _accountRecords = new List<AccountRecord>
        {
            new AccountRecord
            {
                Id = 1.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountRecord
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountRecord
            {
                Id = 3.ToGuid().ToString(),
                EntityId = 2.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            }
        };

        private readonly IEventStoreService<AAccountState> _eventStoreService;

        public EventStoreServiceTests()
        {
            var config = new MapperConfiguration(
                cfg => cfg.AddProfile(new MappingProfile())
            );
            var mapper = config.CreateMapper();
            var repository = new RepositoryMockFactory<AccountRecord>(_accountRecords).GetInstance();
            _eventStoreService = new EventStoreService(
                repository.Object,
                mapper
            );
        }

        [Fact]
        public async Task Should_AppendStateOfEntity_Valid()
        {
            var newState = new AccountApproved
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            };
            var savedEntity = await _eventStoreService.AppendStateOfEntity(newState);

            Assert.NotNull(savedEntity.Id);
        }

        [Fact]
        public async Task ShouldNot_AppendStateOfEntity_Invalid()
        {
            var newState = new AccountApproved();
            var savedEntity = await _eventStoreService.AppendStateOfEntity(newState);

            Assert.NotNull(savedEntity.Id);
        }
    }
}