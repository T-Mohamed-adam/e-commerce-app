
namespace TagerProject.Models.Dtos.PurchaseItem
{
    public class PurchaseItemResponse
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SalesUnitPrice { get; set; }
    }
}
