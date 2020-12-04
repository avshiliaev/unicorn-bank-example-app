using Accounts.Communication.Extensions;
using Accounts.Extensions;
using Accounts.Handlers;
using Accounts.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddCustomDatabase(_configuration)
                .AddDataAccessServices()
                .AddBusinessLogicManagers()
                .AddMessageBus<SubscriptionsHandler>("accounts");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app
                .ConfigureExceptionHandler()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(
                    endpoints => { endpoints.MapControllers(); }
                );
        }
    }
}