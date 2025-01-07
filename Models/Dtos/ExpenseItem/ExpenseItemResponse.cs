
namespace TagerProject.Models.Dtos.ExpenseItem
{
    public class ExpenseItemResponse
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
