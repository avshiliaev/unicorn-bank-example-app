using Sdk.Interfaces;

namespace Sdk.Extensions
{
    public static class ApprovableExtension
    {
        # region Get Property

        public static bool IsApproved(this IApprovable approvable)
        {
            if (!approvable.Approved || approvable.Pending)
                return false;
            return true;
        }

        # endregion

        # region Set Property

        public static IApprovable SetApproval(this IApprovable approvable)
        {
            approvable.Approved = true;
            approvable.Pending = false;

            return approvable;
        }

        public static IApprovable SetPending(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = true;

            return approvable;
        }

        public static IApprovable RevokeApproval(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = true;

            return approvable;
        }

        public static IApprovable SetDenial(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = false;

            return approvable;
        }

        # endregion
    }
}