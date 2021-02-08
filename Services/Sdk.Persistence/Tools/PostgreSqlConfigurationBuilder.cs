using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sdk.Persistence.Tools
{
    public static class PostgreSqlConfigurationBuilder
    {
        public static DbContextOptions<TContext> BuildPostgreSqlConfiguration<TContext>(
            DbContextOptionsBuilder<TContext> optionsBuilder,
            string nameSpace,
            string connectionStringKey
        ) where TContext : DbContext
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, nameSpace)
                )
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    true,
                    true
                )
                .AddEnvironmentVariables()
                .Build();
            optionsBuilder.UseNpgsql(
                configuration.GetConnectionString(connectionStringKey)
            );
            return optionsBuilder.Options;
        }
    }
}