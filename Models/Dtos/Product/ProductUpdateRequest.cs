namespace TagerProject.Models.Dtos.Product
{
    public class ProductUpdateRequest
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int TaxId { get; set; }
        public int UnitId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncludeTax { get; set; }
        public bool IsActive { get; set; }
    }
}
