using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PurchaseUnitPrice { get; set; }
        [Required]
        public decimal SalesUnitPrice { get; set; }
 
        public decimal TotalPurchasePrice { get; set; }

        public decimal TotalSalesPrice { get; set; }

        public decimal ExpectedRevenue { get; set; }
        public string? MembershipNumber { get; set; }

        public virtual Product? Product { get; set; }
    }
}
