using System.Collections.Generic;
using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Managers;
using Approvals.Mappers;
using Approvals.States.Account;
using AutoMapper;
using Sdk.Api.Events;
using Sdk.Api.Events.Domain;
using Sdk.Extensions;
using Sdk.StateMachine.Abstractions;
using Sdk.StateMachine.StateMachines;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Approvals.Tests.Managers
{
    public class StatesManagerTests
    {
        private readonly List<AAccountState> _accountRecords = new List<AAccountState>
        {
            new AccountPending
            {
                Id = 1.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountApproved
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountBlocked
            {
                Id = 3.ToGuid().ToString(),
                EntityId = 2.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            }
        };

        private readonly IStatesManager _statesManager;

        public StatesManagerTests()
        {
            var config = new MapperConfiguration(
                cfg => cfg.AddProfile(new MappingProfile())
            );
            
            var accountContext = new AccountContext();
            var transactionsContext = new TransactionsContext();
            var mapper = config.CreateMapper();
            var eventStoreService = new EventStoreServiceMockFactory<AAccountState>(_accountRecords).GetInstance();
            var publishService = new PublishServiceMockFactory<AAccountState, AccountCreatedEvent>().GetInstance();
            var licenseService = new LicenseServiceMockFactory<AAccountState>().GetInstance();
            
            _statesManager = new StatesManager(
                accountContext,
                transactionsContext,
                mapper,
                eventStoreService.Object,
                licenseService.Object,
                publishService.Object
            );
        }

        [Fact]
        public async Task Should_HandleState_Denied()
        {
            // Arrange
            var newAccountEvent = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            newAccountEvent.SetDenied();
            
            var accountDto = await _statesManager.ProcessAccountState(newAccountEvent);
            Assert.NotNull(accountDto);
            Assert.True(accountDto.IsDenied());
        }
        
        [Fact]
        public async Task Should_HandleState_Approved()
        {
            // Arrange
            var newAccountEvent = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            newAccountEvent.SetApproved();
            
            var accountDto = await _statesManager.ProcessAccountState(newAccountEvent);
            Assert.NotNull(accountDto);
            Assert.True(accountDto.IsApproved());
        }
        
        [Fact]
        public async Task Should_HandleState_Pending()
        {
            // Arrange
            var newAccountEvent = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            newAccountEvent.SetPending();
            
            var accountDto = await _statesManager.ProcessAccountState(newAccountEvent);
            Assert.NotNull(accountDto);
            Assert.True(accountDto.IsPending());
        }
        
        [Fact]
        public async Task Should_HandleState_Blocked()
        {
            // Arrange
            var newAccountEvent = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            newAccountEvent.SetBlocked();
            
            var accountDto = await _statesManager.ProcessAccountState(newAccountEvent);
            Assert.NotNull(accountDto);
            Assert.True(accountDto.IsBlocked());
        }
        
    }
}