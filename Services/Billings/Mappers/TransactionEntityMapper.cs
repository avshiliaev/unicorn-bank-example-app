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
                AccountId = transactionModel.AccountId,
                ProfileId = transactionModel.ProfileId,
                Amount = transactionModel.Amount,
                Info = transactionModel.Info,
                Approved = billingEntity.Approved,
                Timestamp = transactionModel.Timestamp,
                Version = transactionModel.Version,
                SequentialNumber = transactionModel.SequentialNumber
            };
        }
    }
}