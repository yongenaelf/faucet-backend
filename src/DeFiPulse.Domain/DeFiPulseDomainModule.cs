using Volo.Abp.Modularity;

namespace DeFiPulse
{
    [DependsOn(
        typeof(DeFiPulseDomainSharedModule)
    )]
    public class DeFiPulseDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
