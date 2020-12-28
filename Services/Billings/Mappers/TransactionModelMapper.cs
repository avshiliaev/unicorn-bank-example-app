using System;
using Billings.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Billings.Mappers
{
    public static class TransactionModelMapper
    {
        public static BillingEntity ToBillingEntity(this ITransactionModel transactionModel)
        {
            return new BillingEntity
            {
                Id = Guid.NewGuid(),

                ProfileId = transactionModel.ProfileId,
                AccountId = transactionModel.AccountId.ToGuid(),
                TransactionId = transactionModel.Id.ToGuid(),

                Amount = transactionModel.Amount,
                Approved = transactionModel.Approved,
                Pending = transactionModel.Pending,

                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = 0
            };
        }
    }
}