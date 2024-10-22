using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sdk.Tests.Extensions
{
    public static class ConfigureTestAuthentication
    {
        public static IServiceCollection AddTestAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "Test",
                    options => { }
                );

            return services;
        }

        public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
            {
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                var user = new Claim(ClaimTypes.NameIdentifier, "awesome");

                var writeAccounts = new Claim("permissions", "write:accounts");
                var writeTransactions = new Claim("permissions", "write:transactions");

                var identity = new ClaimsIdentity(
                    new[]
                    {
                        writeAccounts,
                        writeTransactions,
                        user
                    },
                    "Test"
                );
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "Test");
                var result = AuthenticateResult.Success(ticket);

                return Task.FromResult(result);
            }
        }
    }
}