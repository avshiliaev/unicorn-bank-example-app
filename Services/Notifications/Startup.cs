using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Extensions;
using Notifications.Handlers;
using Notifications.Hubs;
using Notifications.Persistence.Entities;
using Notifications.Persistence.Repositories;
using Sdk.Api.Extensions;
using Sdk.Auth.Extensions;
using Sdk.Communication.Extensions;
using Sdk.Persistence.Extensions;

namespace Notifications
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
                .AddMongoDb<NotificationsRepository, NotificationEntity>(_configuration)
                .AddAuth0(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<NotificationsSubscriptionsHandler>(_configuration)
                .AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
                app.UseDeveloperExceptionPage();
            }
            
            app
                .ConfigureExceptionHandler()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapHub<NotificationsHub>("/notifications"); });
        }
    }
}