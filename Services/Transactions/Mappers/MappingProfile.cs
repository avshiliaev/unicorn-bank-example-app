using System;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Api.Mappers;
using Transactions.Persistence.Entities;

namespace Accounts.Mappers
{
    public class MappingProfile : BaseMapper<TransactionEntity, ITransactionModel>
    {
        public MappingProfile()
        {
            CreateMap<TransactionEntity, ITransactionModel>();
            CreateMap<ITransactionModel, TransactionEntity>();
        }
    }

    public static class NotificationMapper
    {
        public static NotificationEvent ToNotificationEvent(this TransactionEntity transactionEntity)
        {
            return new NotificationEvent
            {
                Description =
                    $"Your transaction has been {(transactionEntity.Approved ? "processed" : "declined")}.",
                ProfileId = transactionEntity.ProfileId,
                Status = transactionEntity.Approved ? "processed" : "declined",
                TimeStamp = DateTime.Now,
                Title = $"{(transactionEntity.Approved ? "Processing" : "Denial")}",
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}