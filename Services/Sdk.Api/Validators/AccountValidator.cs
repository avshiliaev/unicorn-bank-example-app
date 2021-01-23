using Sdk.Api.Interfaces;

namespace Sdk.Api.Validators
{
    public static class AccountValidator
    {
        public static bool IsValid(this IAccountModel accountModel)
        {
            if (
                string.IsNullOrEmpty(accountModel.ProfileId) ||
                string.IsNullOrEmpty(accountModel.Id)
            )
                return false;
            return true;
        }
    }
}