using System;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
{
    public static class AccountEntityMapper
    {
        public static T ToAccountModel<T>(this AccountEntity accountEntity)
            where T : class, IAccountModel, new()
        {
            return new T
            {
                Id = accountEntity.Id.ToString(),
                Version = accountEntity.Version,
                
                Balance = accountEntity.Balance,
                
                ProfileId = accountEntity.ProfileId,
                
                Approved = accountEntity.Approved,
                Pending = accountEntity.Pending,
                
                LastSequentialNumber = accountEntity.LastSequentialNumber
            };
        }

        public static T ToAccountEvent<T>(this AccountEntity accountEntity)
            where T : class, IAccountModel, IEvent, new()
        {
            return new T
            {
                Id = accountEntity.Id.ToString(),
                Version = accountEntity.Version,
                
                Balance = accountEntity.Balance,
                
                ProfileId = accountEntity.ProfileId,
                
                Approved = accountEntity.Approved,
                Pending = accountEntity.Pending,

                LastSequentialNumber = accountEntity.LastSequentialNumber
            };
        }
    }
}