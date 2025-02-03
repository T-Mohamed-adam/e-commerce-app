using System.ComponentModel.DataAnnotations;
using TagerProject.Models.Dtos.OrderItem;

namespace TagerProject.Models.Dtos.Order
{
    public class OrderAddRequest
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int DiscountId { get; set; }
        public string? CouponCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }

        [Required] 
        public List<OrderItemAddRequest>? OrderItems { get; set; }
    }
}
