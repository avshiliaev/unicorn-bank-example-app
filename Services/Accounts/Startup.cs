using System.Net;
using Accounts.Extensions;
using Accounts.Handlers;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sdk.Api.Extensions;
using Sdk.Auth.Extensions;
using Sdk.Communication.Extensions;
using Sdk.Persistence.Extensions;

namespace Accounts
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
                .AddPostgreSql<AccountsRepository, AccountEntity, AccountsContext>(_configuration)
                .AddAuth0(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<AccountsSubscriptionsHandler>(_configuration);
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
                    endpoints => { endpoints.MapControllers(); }
                )
                .UpdateDatabase<AccountsContext>();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}