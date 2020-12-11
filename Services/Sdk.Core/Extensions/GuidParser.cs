using System;

namespace Sdk.Extensions
{
    public static class GuidParser
    {
        public static Guid ToGuid(this string guidString)
        {
            var isId = Guid.TryParse(guidString, out var id);
            return isId ? id : Guid.NewGuid();
        }
    }
}