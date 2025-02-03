using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Inventory
{
    public class InventoryAddRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public decimal SalesUnitPrice { get; set; }
        public decimal TotalPurchasePrice { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public decimal ExpectedRevenue { get; set; }
    }
}
