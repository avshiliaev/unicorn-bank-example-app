using System;
using Accounts.Persistence.Enums;
using Accounts.Persistence.Models;
using UnicornBank.Sdk.ProtoBuffers;

namespace Accounts.Mappers
{
    public static class AccountEventMapper
    {
        public static AccountModel ToAccountModel(this AccountEvent accountEvent)
        {
            return new AccountModel
            {
                Id = Guid.Parse((ReadOnlySpan<char>) accountEvent.Uuid),
                Balance = accountEvent.Balance,
                ProfileId = Guid.Parse((ReadOnlySpan<char>) accountEvent.Uuid),
                Status = accountEvent.Status
            };
        }

        public static AccountModel ToNewAccountModel(this AccountEvent accountEvent)
        {
            return new AccountModel
            {
                Balance = 0.0f,
                ProfileId = Guid.Parse((ReadOnlySpan<char>) accountEvent.Profile),
                Status = AccountStatus.Pending.ToString(),
                Id = Guid.NewGuid()
            };
        }
    }
}