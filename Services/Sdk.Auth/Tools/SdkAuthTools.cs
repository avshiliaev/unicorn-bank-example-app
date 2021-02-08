using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sdk.Auth.Tools
{
    public static class SdkAuthTools
    {
        public static string GetUserIdentifier(IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;
        }
    }
}