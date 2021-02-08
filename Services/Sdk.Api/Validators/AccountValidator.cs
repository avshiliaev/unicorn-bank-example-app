using Sdk.Interfaces;

namespace Sdk.Api.Validators
{
    public static class AccountValidator
    {
        public static bool IsValid(this IAccountModel accountModel)
        {
            if (
                string.IsNullOrEmpty(accountModel.ProfileId) ||
                string.IsNullOrEmpty(accountModel.EntityId)
            )
                return false;
            return true;
        }
    }
}