using System;
using Accounts.Persistence.Models;
using UnicornBank.Sdk.ProtoBuffers;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static AccountEvent ToAccountEvent(this AccountModel accountModel)
        {
            Enum.TryParse(
                accountModel.Status,
                out AccountEvent.Types.AccountStatus accountStatus
            );
            return new AccountEvent
            {
                Balance = accountModel.Balance,
                Profile = accountModel.ProfileId.ToString(),
                Status = accountStatus,
                Uuid = accountModel.Id.ToString()
            };
        }
    }
}