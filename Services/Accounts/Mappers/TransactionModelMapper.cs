using System;
using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Accounts.Mappers
{
    public static class TransactionModelMapper
    {
        public static TransactionEntity ToTransactionEntity(this ITransactionModel transactionModel)
        {
            // TODO: what to do if not parsed?
            var isId = Guid.TryParse(transactionModel.Id, out var id);
            var isAccountId = Guid.TryParse(transactionModel.Id, out var accountId);
            return new TransactionEntity
            {
                Id = isId ? id : Guid.NewGuid(),
                Amount = transactionModel.Amount,
                AccountId = isAccountId ? accountId : Guid.NewGuid(),
                Status = transactionModel.Status,
                Version = transactionModel.Version
            };
        }
    }
}