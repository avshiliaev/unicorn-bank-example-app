using System.Collections.Generic;
using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Moq;
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

        private readonly IEventStoreManager _manager;

        public ApprovalsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<IAccountModel>().GetInstance();
            var approvalsRepositoryMock = new RepositoryMockFactory<ApprovalEntity>(_approvalEntities).GetInstance();

            _manager = new ApprovalsManager(
                new Mock<ILogger<ApprovalsManager>>().Object,
                new ApprovalsService(approvalsRepositoryMock.Object),
                publishEndpoint.Object
            );
        }

        [Fact]
        public async void Should_EvaluateAccountPendingAsync_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 99.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome"
            };
            accountCheckCommand.SetPending();

            var accountIsCheckedEvent = await _manager
                .SaveStateAndNotifyAsync(accountCheckCommand);
            Assert.NotNull(accountIsCheckedEvent);
            Assert.True(accountIsCheckedEvent.IsPending());
        }
    }
}