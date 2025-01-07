using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.City
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
