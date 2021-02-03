using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Profiles.Extensions;
using Profiles.Handlers;
using Profiles.Hubs;
using Profiles.Persistence.Entities;
using Profiles.Persistence.Repositories;
using Sdk.Api.Extensions;
using Sdk.Auth.Extensions;
using Sdk.Communication.Extensions;
using Sdk.Persistence.Extensions;

namespace Profiles
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddMaterializedViewStore<ProfilesRepository, ProfileEntity>(_configuration)
                .AddAuth0(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<ProfilesSubscriptionsHandler>(_configuration)
                .AddSignalR();
            services.AddHealthChecks().AddCheck("alive", () => HealthCheckResult.Healthy());
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
                app.UseDeveloperExceptionPage();
            }

            app
                .ConfigureExceptionHandler()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapHub<ProfilesHub>("/profiles");
                        endpoints.MapHealthChecks("/health");
                    }
                );
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}