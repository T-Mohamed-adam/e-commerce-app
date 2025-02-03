
using TagerProject.Models.Dtos.PurchaseItem;

namespace TagerProject.Models.Dtos.Purchase
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }
        public int SuppplierId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public string? MembershipNumber { get; set; }
        public string? Status { get; set; }
        public bool Paid { get; set; } = false;

        public List<PurchaseItemResponse>? PurchaseItems { get; set; }
    }
}
