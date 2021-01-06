using System;
using System.Linq;
using Profiles.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Profiles.Mappers
{
    public static class ProfilesModelMapper
    {
        public static ProfileEntity ToProfilesEntity(this IProfileModel<TransactionDto> profileModel)
        {
            return new ProfileEntity
            {
                // Properties
                Balance = profileModel.Balance,
                Transactions = profileModel.Transactions
                    .Select(transaction => new TransactionSubEntity
                    {
                        // Common
                        Id = transaction.Id,
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
                    .ToList(),

                // Foreign Properties
                ProfileId = profileModel.ProfileId,
                AccountId = profileModel.AccountId,

                // Approvable
                Approved = profileModel.Approved,
                Pending = profileModel.Pending,
                Blocked = profileModel.Blocked,

                // Concurrent Host
                LastSequentialNumber = profileModel.LastSequentialNumber,

                // Common
                Id = profileModel.Id,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = profileModel.Version
            };
        }
    }
}