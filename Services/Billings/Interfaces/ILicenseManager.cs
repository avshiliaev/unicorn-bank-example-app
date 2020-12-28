using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Billings.Interfaces
{
    public interface ILicenseManager
    {
        public Task<bool> EvaluateByUserLicenseScope(ITransactionModel transactionModel);
    }
}