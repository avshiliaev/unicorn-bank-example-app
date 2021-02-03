using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Sdk.Api.Extensions;
using Sdk.Api.Interfaces;
using Sdk.Auth.Extensions;
using Sdk.Communication.Extensions;
using Sdk.Extensions;
using Sdk.Persistence.Extensions;
using Transactions.Extensions;
using Transactions.Handlers;
using Transactions.Managers;
using Transactions.Persistence;
using Transactions.Persistence.Entities;
using Transactions.Persistence.Repositories;

namespace Transactions
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
            services.AddControllers();
            services
                .AddCors()
                .AddEventStore<TransactionsRepository, TransactionEntity, TransactionsContext>(_configuration)
                .AddAuth0(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddStateMachine()
                .AddLicenseManager<LicenseManager, ITransactionModel>()
                .AddMessageBus<TransactionsSubscriptionsHandler>(_configuration);
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
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapControllers();
                        endpoints.MapHealthChecks("/health");
                    }
                )
                .UpdateDatabase<TransactionsContext>();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}