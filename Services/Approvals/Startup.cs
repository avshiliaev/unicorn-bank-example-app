using Approvals.Extensions;
using Approvals.Handlers;
using Approvals.Managers;
using Approvals.Mappers;
using Approvals.Persistence;
using Approvals.Persistence.Models;
using Approvals.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Sdk.Api.Extensions;
using Sdk.Communication.Extensions;
using Sdk.Interfaces;
using Sdk.Persistence.Extensions;

namespace Approvals
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddEventStore<ApprovalsRepository, AccountRecord, ApprovalsContext>(_configuration)
                .AddDataAccessServices()
                .AddStateMachine()
                .AddBusinessLogicManagers()
                .AddCustomAutoMapper<MappingProfile>()
                .AddMessageBus<ApprovalsSubscriptionsHandler>(_configuration);
            services.AddHealthChecks().AddCheck("alive", () => HealthCheckResult.Healthy());
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
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); })
                .UpdateDatabase<ApprovalsContext>();
        }
    }
}