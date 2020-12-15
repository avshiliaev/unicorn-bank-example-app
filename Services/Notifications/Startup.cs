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
using Sdk.Communication.Extensions;
using Sdk.Persistence.Extensions;

namespace Notifications
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMongoDb<NotificationsRepository, NotificationEntity>(Configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<NotificationsSubscriptionsHandler>("accounts")
                .AddSignalR();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
                .ConfigureExceptionHandler()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapHub<NotificationsHub>("/notifications"); });
        }
    }
}