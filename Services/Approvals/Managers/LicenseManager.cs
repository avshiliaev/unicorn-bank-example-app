using System.Linq;
using System.Threading.Tasks;
using Approvals.Interfaces;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;

namespace Approvals.Managers
{
    public class LicenseManager : ILicenseManager
    {
        private readonly IApprovalsService _approvalsService;
        private readonly int _maxAccountPerUser = 10;
        private readonly int _maxAccountLowerRange = -500;
        private readonly int _maxAccountUpperRange = (int)1e6;

        public LicenseManager(
            ILogger<LicenseManager> logger,
            IApprovalsService approvalsService
        )
        {
            _approvalsService = approvalsService;
        }

        public async Task<bool> EvaluateCreationAllowedAsync(IAccountModel accountModel)
        {
            var allApprovals = await _approvalsService.GetManyByParameterAsync(
                b =>
                    b!.Approved
                    && b.ProfileId == accountModel.ProfileId
            );

            return allApprovals.Count() <= _maxAccountPerUser;
        }

        public async Task<bool> EvaluateStateAllowedAsync(IAccountModel accountModel)
        {
            if (accountModel.Balance<= _maxAccountUpperRange && accountModel.Balance >= _maxAccountLowerRange)
                return true;
            
            return false;
        }
    }
}