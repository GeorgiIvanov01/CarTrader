using System.ComponentModel.DataAnnotations;
using static CarTrader.GCommon.ValidationConstants;

namespace CarTrader.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Vehicle> Vehicles { get; set; }
            = new HashSet<Vehicle>();
    }
}
