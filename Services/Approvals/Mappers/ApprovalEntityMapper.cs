using Approvals.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Approvals.Mappers
{
    public static class ApprovalEntityMapper
    {
        public static TModel ToAccountModel<TModel>(this ApprovalEntity approvalEntity, IAccountModel accountModel)
            where TModel : class, IAccountModel, new()
        {
            return new TModel
            {
                Id = accountModel.Id,
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId,
                Approved = approvalEntity.Approved,
                Pending = false,
                LastTransactionNumber = accountModel.LastTransactionNumber
            };
        }
    }
}