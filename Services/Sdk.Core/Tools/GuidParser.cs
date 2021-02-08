using System;

namespace Sdk.Tools
{
    public static class GuidParser
    {
        public static Guid ToGuid(string guidString)
        {
            var isId = Guid.TryParse(guidString, out var id);
            return isId ? id : Guid.Empty;
        }
    }
}