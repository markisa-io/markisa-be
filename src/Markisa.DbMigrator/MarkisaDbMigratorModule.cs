using Markisa.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Markisa.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MarkisaEntityFrameworkCoreDbMigrationsModule),
        typeof(MarkisaApplicationContractsModule)
        )]
    public class MarkisaDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
