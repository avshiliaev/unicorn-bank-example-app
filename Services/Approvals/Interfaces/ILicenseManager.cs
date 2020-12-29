using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface ILicenseManager
    {
        public Task<bool> EvaluateByUserLicenseScope(IAccountModel accountModel);
    }
}