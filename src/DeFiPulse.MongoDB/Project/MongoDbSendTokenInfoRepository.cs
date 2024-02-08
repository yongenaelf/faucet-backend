using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DeFiPulse.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace DeFiPulse.Project
{
    public class MongoDbSendTokenInfoRepository: MongoDbRepository<DeFiPulseMongoDbContext, SendTokenInfo, string>,
        ISendTokenInfoRepository
    {
        public MongoDbSendTokenInfoRepository(
            IMongoDbContextProvider<DeFiPulseMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public override Task<SendTokenInfo> GetAsync(string id, bool includeDetails = true,
             CancellationToken cancellationToken = new CancellationToken())
         {
             return base.FindAsync(SendTokenInfo.FormatId(id), includeDetails, cancellationToken);
        }

         public override Task<SendTokenInfo> InsertAsync(SendTokenInfo entity, bool includeDetails = true,
             CancellationToken cancellationToken = new CancellationToken())
         {
             return base.InsertAsync(entity, includeDetails, cancellationToken);
         }
    }
}