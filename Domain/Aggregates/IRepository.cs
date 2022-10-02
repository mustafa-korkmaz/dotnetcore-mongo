

namespace Domain.Aggregates
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument?> GetByIdAsync(string id);

        /// <summary>
        /// Base listing interface without any filters
        /// </summary>
        /// <param name="offset">desired start index for items</param>
        /// <param name="limit">desired length of list items</param>
        /// <returns></returns>
        Task<ListDocumentResponse<TDocument>> ListAsync(int offset, int limit);
        Task InsertOneAsync(TDocument document);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task ReplaceOneAsync(TDocument document);
        Task DeleteByIdAsync(string id);
    }
}
