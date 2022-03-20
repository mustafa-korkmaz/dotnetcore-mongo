namespace Domain.Aggregates.Product
{
    public class Product : Document
    {
        public string Sku { get; private set; }

        public string Name { get; private set; }

        public decimal UnitPrice { get; private set; }

        public Product(string id, string sku, string name, decimal unitPrice) : base(id)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}