
namespace Domain.Aggregates.Order
{
    public class Order : Document
    {
        public string Username { get; private set; }

        public decimal Price => _items.Sum(x => x.GetPrice());

        private readonly List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items;

        public Order(string id, string username) : base(id)
        {
            _items = new List<OrderItem>();
            Username = username;
        }

        public void AddItem(string productId, decimal unitPrice, int quantity)
        {
            _items.Add(new OrderItem(productId, unitPrice, quantity));
        }
    }
}
