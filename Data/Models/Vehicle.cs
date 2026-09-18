using CarTrader.Data.Enums;
using static CarTrader.GCommon.ValidationConstants;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarTrader.Data.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(VehicleMakeMaxLength)]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(VehicleModelMaxLength)]
        public string Model { get; set; } = null!;

        [MaxLength(VehicleDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Mileage must be a positive value.")]
        public int Mileage { get; set; }

        [Required]
        [Range(VehicleEngineSizeMinValue, VehicleEngineSizeMaxValue)]
        public int EngineSize { get; set; }

        [Required]
        public DoorCount Doors { get; set; }

        [Required]
        public FuelType FuelType { get; set; } 

        public TransmissionType TransmissionType { get; set; }

        [Required]
        public VehicleCondition Condition { get; set; }

        [Url]
        [MaxLength(VehicleImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        public string SellerId { get; set; } = null!;
        public virtual IdentityUser Seller { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<UserVehicle> UserVehicles { get; set; }
            = new HashSet<UserVehicle>();
    }
}
