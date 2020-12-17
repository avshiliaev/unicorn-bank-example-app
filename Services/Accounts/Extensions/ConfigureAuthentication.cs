using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Extensions
{
    public static class ConfigureAuthentication
    {
        public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            string domain = "https://unicornbank.eu.auth0.com/";
            string audience = "https://unicornbank.io";

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = audience;
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`.
                    // Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "write:accounts",
                    policy => policy.Requirements.Add(
                        new HasPermissionsRequirement("write:accounts", domain)
                    )
                );
            });
            services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();

            return services;
        }
    }
}