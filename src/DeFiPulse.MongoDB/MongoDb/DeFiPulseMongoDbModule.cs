using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.Uow;

namespace DeFiPulse.MongoDB
{
    [DependsOn(
        typeof(DeFiPulseDomainModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpMongoDbModule)
    )]
    public class DeFiPulseMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<DeFiPulseMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
            
            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });
        }
    }
}
