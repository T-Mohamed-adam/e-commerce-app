namespace TagerProject.Models.Dtos.Package
{
    public class PackageUpdateRequest
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
