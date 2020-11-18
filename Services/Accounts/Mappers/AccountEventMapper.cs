using System;
using Accounts.Persistence.Models;
using Sdk.Api.ViewModels;

namespace Accounts.Mappers
{
    public static class AccountEventMapper
    {
        public static AccountModel ToAccountModel(this AccountEventViewModel accountEvent)
        {
            return new AccountModel
            {
                Id = accountEvent.Id,
                Balance = accountEvent.Balance,
                ProfileId = accountEvent.ProfileId,
                Status = accountEvent.Status
            };
        }

        public static AccountModel ToNewAccountModel(this AccountEventViewModel accountEvent)
        {
            return new AccountModel
            {
                Balance = 0.0f,
                ProfileId = accountEvent.ProfileId,
                Status = "pending",
                Id = Guid.NewGuid()
            };
        }
    }
}