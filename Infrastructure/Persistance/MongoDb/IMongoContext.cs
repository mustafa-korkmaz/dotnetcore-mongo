﻿
using MongoDB.Driver;

namespace Infrastructure.Persistance.MongoDb
{
    public interface IMongoContext : IDisposable
    {
        /// <summary>
        /// in order to support transactional changes
        /// </summary>
        /// <param name="transactionBody"></param>
        /// <returns></returns>
        Task SaveTransactionalChangesAsync(Action transactionBody);
        IMongoCollection<TDocument> GetCollection<TDocument>();
    }
}
