using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sdk.Auth.Extensions
{
    public static class HttpContextAccessorExtension
    {
        public static string GetUserIdentifier(this IHttpContextAccessor httpContextAccessor)
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