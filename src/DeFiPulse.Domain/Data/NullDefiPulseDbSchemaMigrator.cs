using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DeFiPulse.Data
{
    /* This is used if database provider does't DeFine
     * IDeFiPulseDbSchemaMigrator implementation.
     */
    public class NullDeFiPulseDbSchemaMigrator : IDeFiPulseDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}