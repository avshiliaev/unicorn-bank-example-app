using Sdk.Interfaces;

namespace Sdk.Extensions
{
    public static class ConcurrentHostExtension
    {
        public static bool CheckConcurrentController(this IConcurrentHost concurrentHost, IConcurrent concurrent)
        {
            if (concurrent.SequentialNumber != concurrentHost.LastSequentialNumber + 1)
                return false;
            return true;
        }

        public static IConcurrentHost IncrementConcurrentController(this IConcurrentHost concurrentHost)
        {
            concurrentHost.LastSequentialNumber += 1;

            return concurrentHost;
        }
    }
}