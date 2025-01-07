using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public int PackageId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package? Package { get; set; }
        [JsonIgnore]
        public virtual ICollection<Owner>? Owners { get; set; }

    }
}
