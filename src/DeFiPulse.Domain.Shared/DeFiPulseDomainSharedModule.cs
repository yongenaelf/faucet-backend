using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DeFiPulse
{
    public class DeFiPulseDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            DeFiPulseGlobalFeatureConfigurator.Configure();
            DeFiPulseModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DeFiPulseDomainSharedModule>();
            });
        }
    }
}
