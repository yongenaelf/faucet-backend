using Volo.Abp.Modularity;

namespace DeFiPulse
{
    [DependsOn(
        typeof(DeFiPulseApplicationModule),
        typeof(DeFiPulseDomainTestModule)
        )]
    public class DeFiPulseApplicationTestModule : AbpModule
    {

    }
}