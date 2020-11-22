using System;
using Accounts.Dto;
using Accounts.Persistence.Entities;

namespace Accounts.Mappers
{
    public static class AccountDtoMapper
    {
        public static AccountEntity ToAccountEntity(this AccountDto accountEvent)
        {
            // TODO: what to do if not parsed?
            var isId = Guid.TryParse(accountEvent.Id, out var id);
            var isProfileId = Guid.TryParse(accountEvent.Id, out var profileId);
            return new AccountEntity
            {
                Id = isId ? id : Guid.NewGuid(),
                Balance = accountEvent.Balance,
                ProfileId = isProfileId ? profileId : Guid.NewGuid(),
                Status = accountEvent.Status
            };
        }

        public static AccountEntity ToNewAccountEntity(this AccountDto accountEvent)
        {
            var isProfileId = Guid.TryParse(accountEvent.Id, out var profileId);
            return new AccountEntity
            {
                Balance = 0.0f,
                ProfileId = isProfileId ? profileId : Guid.NewGuid(),
                Status = "pending",
                Id = Guid.NewGuid()
            };
        }
    }
}