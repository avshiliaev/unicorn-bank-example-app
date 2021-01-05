using Billings.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Billings.Mappers
{
    public static class BillingEntityMapper
    {
        public static TModel ToTransactionModel<TModel>(
            this BillingEntity billingEntity,
            ITransactionModel transactionModel
        )
            where TModel : class, ITransactionModel, new()
        {
            return new TModel
            {
                Id = transactionModel.Id,
                Version = transactionModel.Version,
                Timestamp = transactionModel.Timestamp,

                AccountId = transactionModel.AccountId,
                ProfileId = transactionModel.ProfileId,

                Amount = transactionModel.Amount,
                Info = transactionModel.Info,

                Approved = billingEntity.Approved,
                Pending = billingEntity.Pending,

                SequentialNumber = transactionModel.SequentialNumber
            };
        }
    }
}