using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistance.MongoDb
{
    public class MongoContext : IMongoContext
    {
        private readonly MongoDbConfig _dbConfig;

        private readonly IMongoDatabase _database;
        private IClientSessionHandle _session;

        private readonly IMongoClient _mongoClient;

        public MongoContext(IMongoClient mongoClient, IOptions<MongoDbConfig> dbConfig)
        {
            _dbConfig = dbConfig.Value;
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(_dbConfig.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        private protected string GetCollectionName(Type documentType)
        {
            var shortType = documentType.Name;

            char[] array = shortType.ToCharArray();

            array[0] = char.ToLower(array[0]);

            var singularCollectionName = new string(array);

            var pluralCollectionName = array[array.Length - 1] == 's' ? singularCollectionName + "es" : singularCollectionName + "s";

            return pluralCollectionName;
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
