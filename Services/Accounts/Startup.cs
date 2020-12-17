using Accounts.Extensions;
using Accounts.Handlers;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sdk.Api.Extensions;
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
                .AddPostgreSql<AccountsRepository, AccountEntity, AccountsContext>(_configuration)
                .AddAuth0(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<AccountsSubscriptionsHandler>(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
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
        }
    }
}