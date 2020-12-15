using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sdk.Persistence.Extensions
{
    public static class PostgreSqlConfigurationBuilder
    {
        public static DbContextOptionsBuilder BuildPostgreSqlConfiguration(
            this DbContextOptionsBuilder optionsBuilder,
            string nameSpace,
            string connectionStringKey
        )
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
            return optionsBuilder;
        }
    }
}