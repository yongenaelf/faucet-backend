using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace DeFiPulse.Data
{
    public class DeFiPulseDbMigrationService : ITransientDependency
    {
        public ILogger<DeFiPulseDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IDeFiPulseDbSchemaMigrator> _dbSchemaMigrators;

        public DeFiPulseDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IDeFiPulseDbSchemaMigrator> dbSchemaMigrators)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;

            Logger = NullLogger<DeFiPulseDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            await MigrateDatabaseSchemaAsync();
            await SeedDataAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }

        private async Task MigrateDatabaseSchemaAsync()
        {
            Logger.LogInformation(
                $"Migrating schema for  database...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }
        }

        private async Task SeedDataAsync()
        {
            Logger.LogInformation($"Executing  database seed...");

            //await _dataSeeder.SeedAsync(tenant?.Id);
        }
    }
}
