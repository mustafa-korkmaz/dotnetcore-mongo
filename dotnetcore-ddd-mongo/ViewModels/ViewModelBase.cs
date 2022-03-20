namespace Presentation.ViewModels
{
    public class ViewModelBase
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ListViewModelRequest
    {
        public int Offset { get; set; }

        public int Limit { get; set; }
    }

    /// <summary>
    /// for specific searches which requires extra filters
    /// </summary>
    /// <typeparam name="TSearchCriteria"></typeparam>
    public class ListViewModelRequest<TSearchCriteria> : ListViewModelRequest
    {
        public TSearchCriteria SearchCriteria { get; set; }
    }

    public class ListViewModelResponse<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TViewModel> Items { get; set; }

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}
