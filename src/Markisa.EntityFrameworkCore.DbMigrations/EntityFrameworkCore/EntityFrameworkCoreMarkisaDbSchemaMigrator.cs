using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Markisa.Data;
using Volo.Abp.DependencyInjection;

namespace Markisa.EntityFrameworkCore
{
    public class EntityFrameworkCoreMarkisaDbSchemaMigrator
        : IMarkisaDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreMarkisaDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the MarkisaMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<MarkisaMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}