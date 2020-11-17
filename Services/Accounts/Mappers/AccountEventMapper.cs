using System;
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
                Status = accountEvent.Status.ToString()
            };
        }

        public static AccountModel ToNewAccountModel(this AccountEvent accountEvent)
        {
            return new AccountModel
            {
                Balance = 0.0f,
                ProfileId = Guid.Parse((ReadOnlySpan<char>) accountEvent.Profile),
                Status = AccountEvent.Types.AccountStatus.Pending.ToString(),
                Id = Guid.NewGuid()
            };
        }
    }
}