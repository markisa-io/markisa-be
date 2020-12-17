using Volo.Abp.Modularity;

namespace Markisa
{
    [DependsOn(
        typeof(MarkisaApplicationModule),
        typeof(MarkisaDomainTestModule)
        )]
    public class MarkisaApplicationTestModule : AbpModule
    {

    }
}