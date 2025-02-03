using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Order
{
    public class OrderCancel
    {
        [Required]
        public int OrderId { get; set; }
        public string? Status { get; set; }
    }
}
