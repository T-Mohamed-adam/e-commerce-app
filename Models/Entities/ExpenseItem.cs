using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class ExpenseItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? NameAr { get; set; }

        [Required]
        public string? NameEn { get; set; }

        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public ICollection<Expense>? Expenses { get; set; }


    }
}
