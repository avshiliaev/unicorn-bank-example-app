using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Profiles.Extensions;
using Profiles.Handlers;
using Profiles.Hubs;
using Sdk.Api.Extensions;
using Sdk.Auth.Extensions;
using Sdk.Communication.Extensions;

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
            ConfigureAuthentication.AddAuth0(services
                    .AddCors()
                    .AddMongoDb<ProfilesRepository, ProfileEntity>(_configuration), _configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<ProfilesSubscriptionsHandler>(_configuration)
                .AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                });
                app.UseDeveloperExceptionPage();
            }

            app
                .ConfigureExceptionHandler()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapHub<ProfilesHub>("/profiles"); });
        }
    }
}