using Approvals.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Approvals.Mappers
{
    public static class ApprovalEntityMapper
    {
        public static TModel ToAccountModel<TModel>(this ApprovalEntity approvalEntity)
            where TModel : class, IAccountModel, new()
        {
            // TODO: issue with not fully defined class instance!
            return new TModel
            {
                Id = approvalEntity.AccountId.ToString(),
                Approved = approvalEntity.Approved,
                Version = approvalEntity.Version
            };
        }
    }
}