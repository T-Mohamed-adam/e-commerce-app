using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Inventory
{
    public class InventoryResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public decimal SalesUnitPrice { get; set; }
        public decimal TotalPurchasePrice { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public decimal ExpectedRevenue { get; set; }
        public string? MembershipNumber { get; set; }

        public virtual TagerProject.Models.Entities.Product? Product { get; set; }
    }
}
