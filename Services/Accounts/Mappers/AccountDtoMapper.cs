using System;
using Accounts.Dto;
using Accounts.Persistence.Entities;

namespace Accounts.Mappers
{
    public static class AccountDtoMapper
    {
        public static AccountEntity ToAccountEntity(this AccountDto accountEvent)
        {
            return new AccountEntity
            {
                Id = accountEvent.Id ?? Guid.NewGuid(),
                Balance = accountEvent.Balance,
                // TODO: what to do if nullable?
                ProfileId = accountEvent.ProfileId ?? Guid.NewGuid(),
                Status = accountEvent.Status
            };
        }

        public static AccountEntity ToNewAccountEntity(this AccountDto accountEvent)
        {
            return new AccountEntity
            {
                Balance = 0.0f,
                ProfileId = accountEvent.ProfileId ?? Guid.NewGuid(),
                Status = "pending",
                Id = Guid.NewGuid()
            };
        }
    }
}