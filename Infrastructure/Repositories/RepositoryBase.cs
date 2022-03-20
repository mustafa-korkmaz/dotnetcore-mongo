using Domain.Aggregates;
using Infrastructure.Persistance.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<TDocument> : IRepository<TDocument> where TDocument : IDocument
    {
        private protected readonly IMongoCollection<TDocument> Collection;
        public RepositoryBase(IMongoContext context)
        {
            Collection = context.GetCollection<TDocument>();
        }

        public async Task<ListDocumentResponse<TDocument>> ListAsync(int offset, int limit)
        {
            var response = new ListDocumentResponse<TDocument>();

            var docs = Collection.Find(new BsonDocument());

            response.TotalCount = await docs.CountDocumentsAsync();

            if (response.TotalCount > 0)
            {
                response.Items = await docs
                    .SortByDescending(p => p.Id)
                    .Skip(offset)
                    .Limit(limit)
                    .ToListAsync();
            }

            return response;
        }

        public Task InsertOneAsync(TDocument document)
        {
            return Collection.InsertOneAsync(document);
        }

        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            return Collection.InsertManyAsync(documents);
        }

        public async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await Collection.FindOneAndReplaceAsync(filter, document);
        }

        public Task<TDocument> GetByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return Collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await Collection.FindOneAndDeleteAsync(filter);
        }
    }
}
