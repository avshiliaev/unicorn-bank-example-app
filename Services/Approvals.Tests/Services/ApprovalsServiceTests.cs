using System;
using System.Collections.Generic;
using Approvals.Interfaces;
using Approvals.Persistence.Entities;
using Approvals.Services;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Approvals.Tests.Services
{
    public class ApprovalsServiceTests
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

        private readonly IApprovalsService _service;

        public ApprovalsServiceTests()
        {
            var approvalsRepositoryMock = new RepositoryMockFactory<ApprovalEntity>(_approvalEntities).GetInstance();
            _service = new ApprovalsService(approvalsRepositoryMock.Object);
        }

        [Fact]
        public async void Should_CreateApprovalAsync_Valid()
        {
            var newAccountEntity = new ApprovalEntity
            {
                AccountId = Guid.NewGuid()
            };
            var newCreatedAccountEntity = await _service.CreateApprovalAsync(newAccountEntity);
            Assert.NotNull(newCreatedAccountEntity);
        }
    }
}