using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sdk.Auth.Handlers;
using Sdk.Auth.Models;
using Sdk.Auth.Requirements;

namespace Sdk.Auth.Extensions
{
    public static class ConfigureAuthentication
    {
        public static IServiceCollection AddAuth0(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var auth0 = new Auth0SettingsModel();
            configuration.GetSection("Auth0").Bind(auth0);
            var auth0Domain = auth0.Domain ?? throw new ArgumentNullException(
                typeof(Auth0SettingsModel).ToString()
            );
            var auth0Audience = auth0.Audience ?? throw new ArgumentNullException(
                typeof(Auth0SettingsModel).ToString()
            );
            var auth0Policies = auth0.Policies ?? throw new ArgumentNullException(
                typeof(Auth0SettingsModel).ToString()
            );

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = auth0Domain;
                    options.Audience = auth0Audience;
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`.
                    // Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };

                    // TODO: https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-5.0
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken))
                                // Read the token out of the query string
                                context.Token = accessToken;
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                foreach (var policyName in auth0Policies)
                    options.AddPolicy(
                        policyName,
                        policy => policy.Requirements.Add(
                            new HasPermissionsRequirement(policyName, auth0Domain)
                        )
                    );
            });
            services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}