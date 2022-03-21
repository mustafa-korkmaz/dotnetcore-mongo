
namespace Domain.Aggregates.Order
{
    public class Order : Document
    {
        public string Username { get; private set; }

        public decimal Price => Items.Sum(x => x.GetPrice());

        //todo Items data cannot be retrieved
        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items { get; private set; }

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
