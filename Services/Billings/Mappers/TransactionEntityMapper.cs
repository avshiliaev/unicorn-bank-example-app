using Billings.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Billings.Mappers
{
    public static class BillingEntityMapper
    {
        public static TModel ToTransactionModel<TModel>(this BillingEntity billingEntity)
            where TModel : class, ITransactionModel, new()
        {
            // TODO: issue with not fully defined class instance!
            return new TModel
            {
                Id = billingEntity.TransactionId.ToString(),
                Approved = billingEntity.Approved,
                Version = billingEntity.Version
            };
        }
    }
}