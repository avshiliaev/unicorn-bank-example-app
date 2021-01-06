using System;
using System.Collections.Generic;
using System.Linq;
using Profiles.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Profiles.Mappers
{
    public static class ProfilesEntityMapper
    {
        public static ProfileDto ToProfileDto(this ProfileEntity profileEntity)
        {

            var transactions = profileEntity.Transactions
                .Select(transaction => new TransactionDto()
                {
                    // Common
                    Id = transaction.Id.ToString(),
                    Version = transaction.Version,

                    // Properties
                    Amount = transaction.Amount,
                    Info = transaction.Info,

                    // Approvable
                    Approved = transaction.Approved,
                    Pending = transaction.Pending,
                    Blocked = transaction.Blocked,

                    // Concurrent
                    SequentialNumber = transaction.SequentialNumber
                })
                .ToList();
            
            return new ProfileDto
            {
                // Properties
                Balance = profileEntity.Balance,
                Transactions = transactions,

                // Foreign Properties
                ProfileId = profileEntity.ProfileId,
                AccountId = profileEntity.AccountId,

                // Approvable
                Approved = profileEntity.Approved,
                Pending = profileEntity.Pending,
                Blocked = profileEntity.Blocked,

                // Concurrent Host
                LastSequentialNumber = profileEntity.LastSequentialNumber,
                
                Id = profileEntity.Id,
                Version = profileEntity.Version
            };
        }
    }
}