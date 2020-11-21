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
                Id = accountEvent.Id,
                Balance = accountEvent.Balance,
                ProfileId = accountEvent.ProfileId,
                Status = accountEvent.Status
            };
        }

        public static AccountEntity ToNewAccountEntity(this AccountDto accountEvent)
        {
            return new AccountEntity
            {
                Balance = 0.0f,
                ProfileId = accountEvent.ProfileId,
                Status = "pending",
                Id = Guid.NewGuid()
            };
        }
    }
}