using System;
using System.Collections.Generic;
using Profiles.Persistence.Entities;
using Sdk.Interfaces;

namespace Profiles.Mappers
{
    public static class AccountModelMapper
    {
        public static ProfileEntity ToNewProfileEntity(this IAccountModel accountEvent)
        {
            return new ProfileEntity
            {
                // Properties
                Balance = accountEvent.Balance,
                Transactions = new List<TransactionSubEntity>(),

                // Foreign Properties
                ProfileId = accountEvent.ProfileId,
                EntityId = accountEvent.Id,

                // Approvable
                Approved = accountEvent.Approved,
                Pending = accountEvent.Pending,
                Blocked = accountEvent.Blocked,

                // Concurrent Host
                LastSequentialNumber = accountEvent.LastSequentialNumber,

                // Common Entity
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = accountEvent.Version
            };
        }

        public static ProfileEntity ToProfileEntity(this IAccountModel accountEvent)
        {
            return new ProfileEntity
            {
                // Properties
                Balance = accountEvent.Balance,
                Transactions = new List<TransactionSubEntity>(),

                // Foreign Properties
                ProfileId = accountEvent.ProfileId,
                EntityId = accountEvent.Id,

                // Approvable
                Approved = accountEvent.Approved,
                Pending = accountEvent.Pending,
                Blocked = accountEvent.Blocked,

                // Concurrent Host
                LastSequentialNumber = accountEvent.LastSequentialNumber,

                // Common Entity
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = accountEvent.Version
            };
        }
    }
}