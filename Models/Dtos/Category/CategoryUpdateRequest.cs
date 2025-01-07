namespace TagerProject.Models.Dtos.Category
{
    public class CategoryUpdateRequest
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsActive { get; set; }
    }
}
