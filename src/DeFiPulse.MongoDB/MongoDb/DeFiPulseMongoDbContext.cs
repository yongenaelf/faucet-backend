
using DeFiPulse.Project;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DeFiPulse.MongoDB
{
    [ConnectionStringName("Default")]
    public class DeFiPulseMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<SendTokenInfo> SendTokenInfo => Collection<SendTokenInfo>();
    }
}
