using Billings.Extensions;
using Billings.Handlers;
using Billings.Managers;
using Billings.Persistence;
using Billings.Persistence.Entities;
using Billings.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Sdk.Api.Extensions;
using Sdk.Api.Interfaces;
using Sdk.Communication.Extensions;
using Sdk.Extensions;
using Sdk.Persistence.Extensions;

namespace Billings
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
            services.AddHealthChecks().AddCheck("alive", () => HealthCheckResult.Healthy());
            services
                .AddCors()
                .AddPostgreSql<BillingsRepository, BillingEntity, BillingsContext>(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddLicenseManager<LicenseManager, ITransactionModel>()
                .AddMessageBus<BillingsSubscriptionsHandler>(_configuration);
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
                .UpdateDatabase<BillingsContext>();
        }
    }
}