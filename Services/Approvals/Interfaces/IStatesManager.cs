using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Approvals.Interfaces
{
    public interface IStatesManager
    {
        public Task<IAccountModel> ProcessAccountState(IAccountModel accountModel);
    }
}