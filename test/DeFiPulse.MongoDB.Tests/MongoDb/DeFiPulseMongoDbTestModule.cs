using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace DeFiPulse.MongoDB
{
    [DependsOn(
        typeof(DeFiPulseTestBaseModule),
        typeof(DeFiPulseMongoDbModule)
        )]
    public class DeFiPulseMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = DeFiPulseMongoDbFixture.ConnectionString.EnsureEndsWith('/')  +
                                   "Db_" +
                                   Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });
        }
    }
}
