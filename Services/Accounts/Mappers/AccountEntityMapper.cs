using Accounts.Dto;
using Accounts.Persistence.Entities;

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
                Id = accountModel.Id.ToString()
            };
        }
    }
}