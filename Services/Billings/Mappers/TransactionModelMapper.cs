using System;
using Billings.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Billings.Mappers
{
    public static class TransactionModelMapper
    {
        public static BillingEntity ToBillingEntity(this ITransactionModel transactionModel, bool isApproved)
        {
            return new BillingEntity
            {
                Id = Guid.NewGuid(),
                TransactionId = transactionModel.Id.ToGuid(),
                ProfileId = transactionModel.ProfileId,
                Approved = isApproved,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = transactionModel.Version
            };
        }
    }
}