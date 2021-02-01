using Sdk.Interfaces;

namespace Sdk.Extensions
{
    public static class ApprovableExtension
    {
        # region Get Property

        public static bool IsApproved(this IApprovable approvable)
        {
            if (approvable.Approved && !approvable.Blocked && !approvable.Pending)
                return true;
            return false;
        }

        public static bool IsPending(this IApprovable approvable)
        {
            if (!approvable.Approved && !approvable.Blocked && approvable.Pending)
                return true;
            return false;
        }

        public static bool IsBlocked(this IApprovable approvable)
        {
            if (!approvable.Approved && approvable.Blocked && !approvable.Pending)
                return true;
            return false;
        }

        public static bool IsDenied(this IApprovable approvable)
        {
            if (!approvable.Blocked && !approvable.Approved && !approvable.Pending)
                return true;
            return false;
        }

        # endregion

        # region Set Property

        public static IApprovable SetApproved(this IApprovable approvable)
        {
            approvable.Approved = true;
            approvable.Pending = false;
            approvable.Blocked = false;

            return approvable;
        }

        public static IApprovable SetPending(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = true;
            approvable.Blocked = false;

            return approvable;
        }

        public static IApprovable SetDenied(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = false;
            approvable.Blocked = false;

            return approvable;
        }

        public static IApprovable SetBlocked(this IApprovable approvable)
        {
            approvable.Approved = false;
            approvable.Pending = false;
            approvable.Blocked = true;

            return approvable;
        }

        # endregion
    }
}