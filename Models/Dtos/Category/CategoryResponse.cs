
namespace TagerProject.Models.Dtos.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? MembershipNumber { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
