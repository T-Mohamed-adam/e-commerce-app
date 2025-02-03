using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.PurchaseItem
{
    public class PurchaseItemAddRequest
    {
        public int PurchaseId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        public decimal SalesUnitPrice { get; set; }
    }
}
