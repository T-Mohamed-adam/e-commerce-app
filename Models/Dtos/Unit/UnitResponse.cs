using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Unit
{
    public class UnitResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
