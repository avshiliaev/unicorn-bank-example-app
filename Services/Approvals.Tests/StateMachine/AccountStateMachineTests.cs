using System.Collections.Generic;
using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events.Local;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Interfaces;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Approvals.Tests.StateMachine
{
    public class AccountStateMachineTests
    {
        private readonly IAccountContext _accountContext;

        private readonly List<AccountEntity> _approvalEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 3.ToGuid().ToString(),
                EntityId = 2.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            }
        };

        public AccountStateMachineTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<IAccountModel>().GetInstance();
            var approvalsRepositoryMock = new RepositoryMockFactory<AccountEntity>(_approvalEntities).GetInstance();
            var licenseManagerMock = new LicenseManagerMockFactory<IAccountModel>().GetInstance();

            var approvalsManager = new ApprovalsManager(
                new Mock<ILogger<ApprovalsManager>>().Object,
                new ApprovalsService(approvalsRepositoryMock.Object),
                publishEndpoint.Object
            );
            _accountContext = new AccountContext(
                approvalsManager,
                licenseManagerMock.Object
            );
        }

        # region State transition with license check

        [Fact]
        public void Should_HandleState_Blocked()
        {
            // Arrange
            var accountCheckCommand = new AccountCheckCommand
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
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountBlocked));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountBlocked));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountBlocked));

            // The License check always returns true.
            _accountContext.CheckLicense(TODO);
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountApproved));
        }

        [Fact]
        public void Should_HandleState_Approved()
        {
            // Arrange
            var accountCheckCommand = new AccountCheckCommand
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
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountApproved));

            // The License check always returns true.
            _accountContext.CheckLicense(TODO);
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountApproved));
        }

        [Fact]
        public void Should_HandleState_Denied()
        {
            // Arrange
            var accountCheckCommand = new AccountCheckCommand
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
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountDenied));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountDenied));

            // The License check always returns true.
            _accountContext.CheckLicense(TODO);
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountApproved));
        }

        [Fact]
        public void Should_HandleState_Pending()
        {
            // Arrange
            var accountCheckCommand = new AccountCheckCommand
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
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            _accountContext.CheckDenied();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            _accountContext.CheckApproved();
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountPending));

            // The License check always returns true.
            _accountContext.CheckLicense(TODO);
            Assert.True(_accountContext.GetCurrentState() == typeof(AccountApproved));
        }

        # endregion
    }
}