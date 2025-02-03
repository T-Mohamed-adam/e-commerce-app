using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.OrderItem
{
    public class OrderItemAddRequest
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}
