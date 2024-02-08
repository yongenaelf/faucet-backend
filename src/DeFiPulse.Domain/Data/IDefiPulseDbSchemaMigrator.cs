using System.Threading.Tasks;

namespace DeFiPulse.Data
{
    public interface IDeFiPulseDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
