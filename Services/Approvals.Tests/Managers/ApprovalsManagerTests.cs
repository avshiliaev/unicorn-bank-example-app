using System.Collections.Generic;
using Approvals.Interfaces;
using Approvals.Managers;
using Approvals.Persistence.Entities;
using Approvals.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Approvals.Tests.Managers
{
    public class ApprovalsManagerTests
    {
        private readonly List<ApprovalEntity> _approvalEntities = new List<ApprovalEntity>
        {
            new ApprovalEntity
            {
                Id = 1.ToGuid(),
                Approved = true,
                AccountId = 1.ToGuid(),
                Version = 0
            },
            new ApprovalEntity
            {
                Id = 2.ToGuid(),
                Approved = false,
                AccountId = 1.ToGuid(),
                Version = 0
            },
            new ApprovalEntity
            {
                Id = 3.ToGuid(),
                Approved = true,
                AccountId = 2.ToGuid(),
                Version = 0
            }
        };

        private readonly IApprovalsManager _manager;

        public ApprovalsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<IAccountModel>().GetInstance();
            var approvalsRepositoryMock = new RepositoryMockFactory<ApprovalEntity>(_approvalEntities).GetInstance();
            var licenseManagerMock = new LicenseManagerMockFactory<IAccountModel>().GetInstance();

            _manager = new ApprovalsManager(
                new Mock<ILogger<ApprovalsManager>>().Object,
                new ApprovalsService(approvalsRepositoryMock.Object),
                publishEndpoint.Object,
                licenseManagerMock.Object
            );
        }
    
        # region EvaluateAccountPendingAsync
        
        [Fact]
        public async void Should_EvaluateAccountPendingAsync_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetPending();
            var accountIsCheckedEvent = await _manager
                .EvaluateAccountPendingAsync(accountCheckCommand);
            Assert.NotNull(accountIsCheckedEvent);
            Assert.True(accountIsCheckedEvent.IsApproved());
        }

        [Fact]
        public async void ShouldNot_EvaluateAccountPendingAsync_Invalid()
        {
            var accountCheckCommand = new AccountCheckCommand();
            accountCheckCommand.SetPending();
            var accountIsCheckedEvent = await _manager
                .EvaluateAccountPendingAsync(accountCheckCommand);
            Assert.Null(accountIsCheckedEvent);
        }
        # endregion
        
        # region EvaluateAccountRunningAsync
        
        [Fact]
        public async void Should_EvaluateAccountRunningAsync_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetApproval();
            var accountIsCheckedEvent = await _manager
                .EvaluateAccountRunningAsync(accountCheckCommand);
            Assert.NotNull(accountIsCheckedEvent);
        }

        [Fact]
        public async void ShouldNot_EvaluateAccountRunningAsync_Invalid()
        {
            var accountCheckCommand = new AccountCheckCommand();
            accountCheckCommand.SetApproval();
            var accountIsCheckedEvent = await _manager
                .EvaluateAccountRunningAsync(accountCheckCommand);
            Assert.Null(accountIsCheckedEvent);
        }
        # endregion
    }
}