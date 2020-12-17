using Markisa.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Markisa
{
    [DependsOn(
        typeof(MarkisaEntityFrameworkCoreTestModule)
        )]
    public class MarkisaDomainTestModule : AbpModule
    {

    }
}