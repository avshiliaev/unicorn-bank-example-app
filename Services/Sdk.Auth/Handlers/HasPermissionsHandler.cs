using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sdk.Auth.Requirements;

namespace Sdk.Auth.Handlers
{
    public class HasPermissionsHandler : AuthorizationHandler<HasPermissionsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            HasPermissionsRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var permissions = context.User
                .FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer)
                .ToList();
            // Succeed if the scope array contains the required scope
            if (permissions.Any(p => p.Value == requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}