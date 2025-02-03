using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagerProject.Models.Entities
{
    public class PurchaseItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        public decimal SalesUnitPrice { get; set; } = 0;

        [ForeignKey("PurchaseId")]
        public virtual Purchase? Purchase { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
