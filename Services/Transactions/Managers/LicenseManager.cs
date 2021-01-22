using System;
using System.Threading.Tasks;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Abstractions;
using Transactions.Interfaces;

namespace Transactions.Managers
{
    public class LicenseManager : ALicenseManager<ITransactionModel>
    {
        private readonly IAccountsService _accountsService;

        public LicenseManager(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public override async Task<bool> EvaluateNewEntityAsync(ITransactionModel model)
        {
            var account = await _accountsService.GetOneByParameterAsync(
                a =>
                    a.Id == model.AccountId.ToGuid() &&
                    a.ProfileId == model.ProfileId
            );
            if (account == null)
                return false;
            return !account.IsBlocked();
        }

        public override Task<bool> EvaluateStateEntityAsync(ITransactionModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}