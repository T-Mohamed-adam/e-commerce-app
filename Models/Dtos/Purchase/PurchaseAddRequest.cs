using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TagerProject.Models.Dtos.PurchaseItem;

namespace TagerProject.Models.Dtos.Purchase
{
    public class PurchaseAddRequest
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int SuppplierId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Notes { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public bool Paid { get; set; }

        [Required]
        public List<PurchaseItemAddRequest>? PurchaseItems { get; set; }
    }
}
