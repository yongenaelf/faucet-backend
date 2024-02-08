using System;
using Mongo2Go;

namespace DeFiPulse.MongoDB
{
    public class DeFiPulseMongoDbFixture : IDisposable
    {
        private static readonly MongoDbRunner MongoDbRunner;
        public static readonly string ConnectionString;

        static DeFiPulseMongoDbFixture()
        {
            MongoDbRunner = MongoDbRunner.Start();
            ConnectionString = MongoDbRunner.ConnectionString;
        }

        public void Dispose()
        {
            MongoDbRunner?.Dispose();
        }
    }
}
