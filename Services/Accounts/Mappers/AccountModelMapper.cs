using System;
using Accounts.Dto;
using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static AccountEntity ToAccountEntity(this IAccountModel accountEvent)
        {
            // TODO: what to do if not parsed?
            var isId = Guid.TryParse(accountEvent.Id, out var id);
            var isProfileId = Guid.TryParse(accountEvent.Id, out var profileId);
            return new AccountEntity
            {
                Id = isId ? id : Guid.NewGuid(),
                Balance = accountEvent.Balance,
                ProfileId = isProfileId ? profileId : Guid.NewGuid(),
                Status = accountEvent.Status,
                Version = accountEvent.Version
            };
        }

        public static AccountEntity ToNewAccountEntity(this IAccountModel accountEvent)
        {
            var isProfileId = Guid.TryParse(accountEvent.Id, out var profileId);
            return new AccountEntity
            {
                Balance = 0.0f,
                ProfileId = isProfileId ? profileId : Guid.NewGuid(),
                Status = "pending",
                Id = Guid.NewGuid(),
                Version = 0
            };
        }
    }
}