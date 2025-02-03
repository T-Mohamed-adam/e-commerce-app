
using TagerProject.Models.Dtos.OrderItem;

namespace TagerProject.Models.Dtos.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public int DiscountId { get; set; }
        public string? CouponCode { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string? MembershipNumber { get; set; }
        public string? Status { get; set; }
        public bool Paid { get; set; }
        public List<OrderItemResponse>? OrderItems { get; set; }
    }
}
