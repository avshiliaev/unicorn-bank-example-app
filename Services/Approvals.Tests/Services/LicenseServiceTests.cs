using System.Threading.Tasks;
using Approvals.Services;
using Approvals.States.Account;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;
using Sdk.Tests.Extensions;
using Xunit;

namespace Approvals.Tests.Services
{
    public class LicenseServiceTests
    {
        private readonly ILicenseService<AAccountState> _licenseService;

        public LicenseServiceTests()
        {
            _licenseService = new LicenseService();
        }

        [Fact]
        public async Task Should_EvaluatePendingAsync_Valid()
        {
            var newState = new AccountPending()
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            };
            var isAllowed = await _licenseService.EvaluatePendingAsync(newState);
            Assert.True(isAllowed);
        }
        
        [Fact]
        public async Task Should_EvaluateNotPendingAsync_Valid()
        {
            var newState = new AccountApproved()
            {
                Id = 2.ToGuid().ToString(),
                EntityId = 1.ToGuid().ToString(),
                ProfileId = 1.ToGuid().ToString(),
                Version = 0
            };
            var isAllowed = await _licenseService.EvaluateNotPendingAsync(newState);
            Assert.True(isAllowed);
        }
    }
}