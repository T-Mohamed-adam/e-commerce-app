using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.OrderItem
{
    public class OrderItemResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
