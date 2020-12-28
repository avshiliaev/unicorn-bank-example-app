using System.Linq;
using System.Threading.Tasks;
using Billings.Interfaces;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Billings.Managers
{
    public class LicenseManager : ILicenseManager
    {
        private readonly IBillingsService _billingsService;

        public LicenseManager(
            ILogger<LicenseManager> logger,
            IBillingsService billingsService
        )
        {
            _billingsService = billingsService;
        }

        public async Task<bool> EvaluateByUserLicenseScope(ITransactionModel transactionModel)
        {
            var allTransactions = await _billingsService.GetManyByParameterAsync(
                b => b!.Approved && b.AccountId == transactionModel.AccountId.ToGuid()
            );
            var lastTransactionNumber = allTransactions.Max(t => t?.Amount);

            return false;
        }
    }
}