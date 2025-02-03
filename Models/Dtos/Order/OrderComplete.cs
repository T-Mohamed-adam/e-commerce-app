using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Order
{
    public class OrderComplete
    {
        [Required]
        public int OrderId { get; set; }
        public string? Status { get; set; }
    }
}
