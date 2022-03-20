﻿
using Domain.Aggregates;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Returns document repository inherited from  IRepository
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository, TDocument>()
            where TDocument : IDocument
            where TRepository : IRepository<TDocument>;
    }
}
