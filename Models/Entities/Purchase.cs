using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagerProject.Models.Entities
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; } 
        public Guid Guid { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int SuppplierId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Notes { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public string? MembershipNumber { get; set; }

        public string? Status { get; set; } = "Pending";
        public bool Paid { get; set; } = false;

        [ForeignKey("SuppplierId")]
        public virtual Supplier? Supplier { get; set; }

        public virtual ICollection<PurchaseItem>? PurchaseItems { get; set; }

    }
}
