using DeFiPulse.MongoDB;
using Volo.Abp.Modularity;

namespace DeFiPulse
{
    [DependsOn(
        typeof(DeFiPulseMongoDbTestModule)
        )]
    public class DeFiPulseDomainTestModule : AbpModule
    {

    }
}