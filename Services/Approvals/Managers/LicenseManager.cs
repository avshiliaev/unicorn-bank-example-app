using System.Linq;
using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;
using Sdk.License.Abstractions;

namespace Approvals.Managers
{
    public class LicenseManager : ALicenseManager<IAccountModel>
    {
        private readonly IEventStoreService<ApprovalEntity> _approvalsService;
        private readonly int _maxAccountLowerRange = -500;
        private readonly int _maxAccountPerUser = 10;
        private readonly int _maxAccountUpperRange = (int) 1e6;

        public LicenseManager(
            ILogger<LicenseManager> logger,
            IEventStoreService<ApprovalEntity> approvalsService
        )
        {
            _approvalsService = approvalsService;
        }

        public override async Task<bool> EvaluatePendingAsync(IAccountModel accountModel)
        {
            var allApprovals = await _approvalsService.GetManyLastVersionAsync(
                b =>
                    b!.Approved
                    && b.ProfileId == accountModel.ProfileId
            );

            return allApprovals.Count() <= _maxAccountPerUser;
        }

        public override Task<bool> EvaluateNotPendingAsync(IAccountModel accountModel)
        {
            if (accountModel.Balance <= _maxAccountUpperRange && accountModel.Balance >= _maxAccountLowerRange)
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}