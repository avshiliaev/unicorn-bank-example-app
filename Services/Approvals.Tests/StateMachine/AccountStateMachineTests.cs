using System.Collections.Generic;
using Approvals.Interfaces;
using Approvals.Managers;
using Approvals.Persistence.Entities;
using Approvals.Services;
using Approvals.StateMachine;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;
using Approvals.StateMachine.States;

namespace Approvals.Tests.StateMachine
{
    public class AccountStateMachineTests
    {
        private readonly IAccountContext _accountContext;

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

        public AccountStateMachineTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<IAccountModel>().GetInstance();
            var approvalsRepositoryMock = new RepositoryMockFactory<ApprovalEntity>(_approvalEntities).GetInstance();
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
        
        # region Simple State Transform
        [Fact]
        public void Should_HandleStateBlocked_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetBlocked();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckPending();
            
            Assert.True(_accountContext.IsBlocked());
        }
        
        [Fact]
        public void Should_HandleStateApproved_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetApproval();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            Assert.True(_accountContext.IsApproved());
        }
        
        [Fact]
        public void Should_HandleStateDenied_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetDenial();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            Assert.True(!_accountContext.IsApproved());
        }
        
        [Fact]
        public void Should_HandleStatePending_Valid()
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
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            Assert.True(_accountContext.IsPending());
        }
        # endregion
        
        # region State Transform with license check
        [Fact]
        public void Should_HandleStateBlocked_License_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetBlocked();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            // The License check always returns true.
            _accountContext.CheckLicense();
            
            Assert.True(_accountContext.IsApproved());
        }
        
        [Fact]
        public void Should_HandleStateApproved_License_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetApproval();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            // The License check always returns true.
            _accountContext.CheckLicense();
            
            Assert.True(_accountContext.IsApproved());
        }
        
        [Fact]
        public void Should_HandleStateDenied_License_Valid()
        {
            var accountCheckCommand = new AccountCheckCommand
            {
                Version = 0,
                Id = 1.ToGuid().ToString(),
                Balance = 0f,
                ProfileId = "awesome",
                Approved = false
            };
            accountCheckCommand.SetDenial();
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            // The License check always returns true.
            _accountContext.CheckLicense();
            
            Assert.True(_accountContext.IsApproved());
        }
        
        [Fact]
        public void Should_HandleStatePending_License_Valid()
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
            
            _accountContext.InitializeState(new AccountPending(), accountCheckCommand);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckPending();
            
            // The License check always returns true.
            _accountContext.CheckLicense();
            
            Assert.True(_accountContext.IsApproved());
        }
        # endregion
    }

}