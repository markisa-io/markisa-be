using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Markisa.Data
{
    /* This is used if database provider does't define
     * IMarkisaDbSchemaMigrator implementation.
     */
    public class NullMarkisaDbSchemaMigrator : IMarkisaDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}