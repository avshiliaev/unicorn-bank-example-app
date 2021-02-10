using System.Collections.Generic;
using System.Threading.Tasks;
using Approvals.States.Account;
using Moq;
using Sdk.Api.Events;
using Sdk.Api.Events.Domain;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;
using Sdk.StateMachine.Interfaces;
using Sdk.StateMachine.StateMachines;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Approvals.Tests.States.Accounts
{
    public class AccountStatesTests
    {
        private readonly IAccountContext _accountContext;

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

        private readonly Mock<IEventStoreService<AAccountState>> _eventStoreService;
        private readonly Mock<ILicenseService<AAccountState>> _licenseService;
        private readonly Mock<IPublishService<AAccountState>> _publishService;

        public AccountStatesTests()
        {
            _eventStoreService = new EventStoreServiceMockFactory<AAccountState>(_accountRecords).GetInstance();
            _publishService = new PublishServiceMockFactory<AAccountState, AccountCreatedEvent>().GetInstance();
            _licenseService = new LicenseServiceMockFactory<AAccountState>().GetInstance();
            _accountContext = new AccountContext();
        }

        # region State transition with license check

        [Fact]
        public async Task Should_HandleState_Blocked()
        {
            // Arrange
            var accountCheckCommand = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetBlocked();
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);

            // Act / Assert
            _accountContext.CheckBlocked();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountBlocked));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountBlocked));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountBlocked));

            // The License check always returns true.
            await _accountContext.CheckLicense(_licenseService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PreserveState(_eventStoreService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PublishEvent(_publishService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));
        }

        [Fact]
        public async Task Should_HandleState_Approved()
        {
            // Arrange
            var accountCheckCommand = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetApproved();
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);

            // Act / Assert
            _accountContext.CheckBlocked();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            // The License check always returns true.
            await _accountContext.CheckLicense(_licenseService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PreserveState(_eventStoreService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PublishEvent(_publishService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));
        }

        [Fact]
        public async Task Should_HandleState_Denied()
        {
            // Arrange
            var accountCheckCommand = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetDenied();
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);

            // Act / Assert
            _accountContext.CheckBlocked();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountDenied));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountDenied));

            // The License check always returns true.
            await _accountContext.CheckLicense(_licenseService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PreserveState(_eventStoreService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PublishEvent(_publishService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));
        }

        [Fact]
        public async Task Should_HandleState_Pending()
        {
            // Arrange
            var accountCheckCommand = new AccountProcessedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetPending();
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);

            // Act / Assert
            _accountContext.CheckBlocked();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountPending));

            // The License check always returns true.
            await _accountContext.CheckLicense(_licenseService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PreserveState(_eventStoreService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));

            await _accountContext.PublishEvent(_publishService.Object);
            Assert.True(_accountContext.GetCurrentState().GetType() == typeof(AccountApproved));
        }

        # endregion
    }
}