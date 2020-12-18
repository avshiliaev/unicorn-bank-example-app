using System;
using Microsoft.AspNetCore.Authorization;

namespace Sdk.Auth.Requirements
{
    public class HasPermissionsRequirement : IAuthorizationRequirement
    {
        public HasPermissionsRequirement(string permission, string issuer)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }

        public string Issuer { get; }
        public string Permission { get; }
    }
}