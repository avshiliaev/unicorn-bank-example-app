using Accounts.Dto;
using Accounts.Persistence.Entities;
using Sdk.Api.Events;

namespace Accounts.Mappers
{
    public static class AccountEntityMapper
    {
        public static AccountDto ToAccountDto(this AccountEntity accountModel)
        {
            return new AccountDto
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId.ToString(),
                Status = accountModel.Status,
                Id = accountModel.Id.ToString(),
                Version = accountModel.Version
            };
        }

        public static AccountCreatedEvent ToAccountCreatedEvent(this AccountEntity accountModel)
        {
            return new AccountCreatedEvent
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId.ToString(),
                Status = accountModel.Status,
                Id = accountModel.Id.ToString(),
                Version = accountModel.Version
            };
        }

        public static AccountUpdatedEvent ToAccountUpdatedEvent(this AccountEntity accountModel)
        {
            return new AccountUpdatedEvent
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId.ToString(),
                Status = accountModel.Status,
                Id = accountModel.Id.ToString(),
                Version = accountModel.Version
            };
        }
    }
}