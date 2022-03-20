using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Order
{
    public class AddEditOrderViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public IReadOnlyCollection<AddEditOrderItemViewModel> Items { get; set; }
    }

    public class AddEditOrderItemViewModel
    {
        [Required]
        public string ProductId { get; private set; }

        [Required]
        public decimal UnitPrice { get; private set; }

        [Required]
        public int Quantity { get; private set; }
    }
}
