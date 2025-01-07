using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Tax
{
    public class TaxResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int Value { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
