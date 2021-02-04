using System;
using System.Threading.Tasks;
using Billings.Persistence.Entities;
using Sdk.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Persistence.Interfaces;

namespace Billings.Managers
{
    public class LicenseManager : ALicenseManager<ITransactionModel>
    {
        private readonly IEventStoreService<BillingEntity> _eventStoreService;
        private readonly int _maxTransactionsPerDay = 100;

        public LicenseManager(
            IEventStoreService<BillingEntity> eventStoreService
        )
        {
            _eventStoreService = eventStoreService;
        }

        public override async Task<bool> EvaluatePendingAsync(ITransactionModel transactionModel)
        {
            var allTransactions = await _eventStoreService.GetAllEntitiesLastStatesAsync(
                b =>
                    b!.Approved
                    && b.AccountId == transactionModel.AccountId.ToGuid()
                    && b.Created.Date == DateTime.Today
            );
            var transactionsToday = allTransactions.Count;

            return transactionsToday < _maxTransactionsPerDay;
        }

        public override Task<bool> EvaluateNotPendingAsync(ITransactionModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}