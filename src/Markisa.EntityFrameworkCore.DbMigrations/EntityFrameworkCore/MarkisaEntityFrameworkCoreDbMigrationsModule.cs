using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Markisa.EntityFrameworkCore
{
    [DependsOn(
        typeof(MarkisaEntityFrameworkCoreModule)
        )]
    public class MarkisaEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MarkisaMigrationsDbContext>();
        }
    }
}
