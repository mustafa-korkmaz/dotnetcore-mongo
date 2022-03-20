
namespace Application.Dto.Product
{
    public class ProductDto : DtoBase
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
