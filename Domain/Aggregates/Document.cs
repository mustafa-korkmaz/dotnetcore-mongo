

namespace Domain.Aggregates
{
    public class Document : IDocument
    {
        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Document(string id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
    }

    public interface IDocument
    {
        public string Id { get; }

        DateTime CreatedAt { get; }
    }

    public class ListDocumentRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }

    public class ListDocumentResponse<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDocument> Items { get; set; } = new List<TDocument>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }
}