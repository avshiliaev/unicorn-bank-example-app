using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accounts.Persistence
{
    public class AccountsContextFactory : IDesignTimeDbContextFactory<AccountsContext>
    {
        public AccountsContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Accounts")
                )
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", 
                    true,
                    true
                )
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
            optionsBuilder.UseNpgsql(
                configuration.GetConnectionString("AccountsContext")
            );
            return new AccountsContext(optionsBuilder.Options);
        }
    }
}