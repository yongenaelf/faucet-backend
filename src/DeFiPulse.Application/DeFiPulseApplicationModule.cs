
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DeFiPulse
{
    [DependsOn(
        typeof(DeFiPulseDomainModule),
        typeof(DeFiPulseApplicationContractsModule),
        typeof(AbpLocalizationModule),
        typeof(AbpVirtualFileSystemModule)
    )]
    public class DeFiPulseApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<ApiConfigOptions>(configuration.GetSection("ApiConfig"));
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<DeFiPulseApplicationModule>();
            });
            
        }
    }
}
