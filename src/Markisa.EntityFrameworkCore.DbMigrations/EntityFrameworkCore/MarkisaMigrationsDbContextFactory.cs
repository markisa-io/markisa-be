using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Markisa.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class MarkisaMigrationsDbContextFactory : IDesignTimeDbContextFactory<MarkisaMigrationsDbContext>
    {
        public MarkisaMigrationsDbContext CreateDbContext(string[] args)
        {
            MarkisaEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MarkisaMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new MarkisaMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Markisa.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
