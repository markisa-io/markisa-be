using System.Threading.Tasks;

namespace Markisa.Data
{
    public interface IMarkisaDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
