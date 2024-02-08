using Volo.Abp.Modularity;

namespace DeFiPulse
{
    [DependsOn(
        typeof(DeFiPulseDomainSharedModule)
    )]
    public class DeFiPulseApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            DeFiPulseDtoExtensions.Configure();
        }
    }
}
