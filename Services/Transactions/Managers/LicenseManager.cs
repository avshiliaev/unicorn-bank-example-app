using System;
using System.Threading.Tasks;
using Sdk.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Persistence.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Managers
{
    public class LicenseManager : ALicenseManager<ITransactionModel>
    {
        private readonly IEventStoreService<TransactionEntity> _eventStoreService;

        public LicenseManager(IEventStoreService<TransactionEntity> eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public override async Task<bool> EvaluatePendingAsync(ITransactionModel model)
        {
            var account = await _eventStoreService.GetOneEntityLastStateAsync(
                a =>
                    a.Id == model.AccountId.ToGuid() &&
                    a.ProfileId == model.ProfileId
            );
            if (account == null)
                return false;
            return !account.IsBlocked();
        }

        public override Task<bool> EvaluateNotPendingAsync(ITransactionModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}