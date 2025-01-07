
namespace TagerProject.Models.Dtos.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int TaxId { get; set; }
        public int UnitId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncludeTax { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string? CategoryName { get; set; }

        public virtual TagerProject.Models.Entities.Category? Category { get; set; }
        public virtual TagerProject.Models.Entities.Tax? Tax { get; set; }
    }
}
