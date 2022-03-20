
using MongoDB.Driver;

namespace Infrastructure.Persistance.MongoDb
{
    public interface IMongoContext : IDisposable
    {
        Task<int> SaveChanges();
        IMongoCollection<TDocument> GetCollection<TDocument>();
    }
}
