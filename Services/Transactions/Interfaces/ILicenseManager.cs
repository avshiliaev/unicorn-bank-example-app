using System;
using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Transactions.Interfaces
{
    public interface ILicenseManager
    {
        public Task<IAccountModel?> UpdateAccountStateAsync(IAccountModel accountModel);
        public Task<bool> CheckAccountStateAsync(Guid accountId);
    }
}