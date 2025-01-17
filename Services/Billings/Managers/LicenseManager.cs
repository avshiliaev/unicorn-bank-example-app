using System;
using System.Linq;
using System.Threading.Tasks;
using Billings.Interfaces;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Abstractions;

namespace Billings.Managers
{
    public class LicenseManager : ALicenseManager<ITransactionModel>
    {
        private readonly IBillingsService _billingsService;
        private readonly int _maxTransactionsPerDay = 100;

        public LicenseManager(
            ILogger<LicenseManager> logger,
            IBillingsService billingsService
        )
        {
            _billingsService = billingsService;
        }

        public override async Task<bool> EvaluateNewEntityAsync(ITransactionModel transactionModel)
        {
            var allTransactions = await _billingsService.GetManyByParameterAsync(
                b =>
                    b!.Approved
                    && b.AccountId == transactionModel.AccountId.ToGuid()
                    && b.Created.Date == DateTime.Today
            );
            var transactionsToday = allTransactions.Count();

            return transactionsToday < _maxTransactionsPerDay;
        }

        public override Task<bool> EvaluateStateEntityAsync(ITransactionModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}