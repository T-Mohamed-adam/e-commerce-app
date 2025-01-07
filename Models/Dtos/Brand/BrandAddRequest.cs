namespace TagerProject.Models.Dtos.Brand
{
    public class BrandAddRequest
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
