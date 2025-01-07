namespace TagerProject.Models.Dtos.Supplier
{
    public class SupplierUpdateRequest
    {
        public int CityId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TIN { get; set; }
        public string? CommercialRegister { get; set; }
        public bool IsActive { get; set; }
    }
}
