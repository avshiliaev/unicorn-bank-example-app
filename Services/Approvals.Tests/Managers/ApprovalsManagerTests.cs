using System.Collections.Generic;
using Approvals.Interfaces;
using Approvals.Managers;
using Approvals.Persistence.Entities;
using Approvals.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
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

            _manager = new ApprovalsManager(
                new Mock<ILogger<ApprovalsManager>>().Object,
                new ApprovalsService(approvalsRepositoryMock.Object),
                publishEndpoint.Object
            );
        }

        [Fact]
        public async void ShouldSuccessfullyCreateANewApproval()
        {
            var accountCreatedEvent = new AccountCreatedEvent
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            var newCreatedApproval = await _manager
                .EvaluateAccountAsync(accountCreatedEvent);
            Assert.NotNull(newCreatedApproval);
        }

        [Fact]
        public async void ShouldNotCreateAnInvalidApproval()
        {
            var accountCreatedEvent = new AccountCreatedEvent();
            var newCreatedApproval = await _manager
                .EvaluateAccountAsync(accountCreatedEvent);
            Assert.Null(newCreatedApproval);
        }
    }
}