using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Package
{
    public class PackageResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
