using DeFiPulse.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DeFiPulse.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DeFiPulseMongoDbModule),
        typeof(DeFiPulseApplicationContractsModule)
        )]
    public class DeFiPulseDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
