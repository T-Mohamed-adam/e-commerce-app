namespace TagerProject.Models.Dtos.Tax
{
    public class TaxAddRequest
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int Value { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
