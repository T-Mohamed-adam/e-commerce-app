using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int? DiscountId { get; set; }
        public string? CouponCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string? MembershipNumber { get; set; }
        public string? Status { get; set; } = "Pending";
        public bool Paid { get; set; } = false;
        
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("DiscountId")]
        public virtual Discount? Discount { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
