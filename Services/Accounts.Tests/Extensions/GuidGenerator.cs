using System;

namespace Accounts.Tests.Extensions
{
    public static class GuidGenerator
    {
        public static Guid ToGuid(this int simpleInteger)
        {
            if (simpleInteger < 0 | simpleInteger > 9) return Guid.Empty;
            var guidStingTemplate = $"00000000-0000-0000-0000-00000000000{simpleInteger}";
            return Guid.Parse(guidStingTemplate);
        }
    }
}