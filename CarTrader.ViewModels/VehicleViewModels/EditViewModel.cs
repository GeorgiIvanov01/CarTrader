using CarTrader.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static CarTrader.GCommon.ValidationConstants;

namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(VehicleMakeMaxLength, MinimumLength = VehicleMakeMinLength)]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(VehicleModelMaxLength, MinimumLength = VehicleModelMinLength)]
        public string Model { get; set; } = null!;

        [StringLength(VehicleDescriptionMaxLength, MinimumLength = VehicleDescriptionMinLength)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required]
        [Range(VehicleYearMinValue, VehicleYearMaxValue, ErrorMessage = "Year is not valid.")]
        public int Year { get; set; }

        [Required]
        [Range(VehicleMileageMinValue, VehicleMileageMaxValue, ErrorMessage = "Mileage is not valid.")]
        public int Mileage { get; set; }

        [Required]
        [Range(VehicleEngineSizeMinValue, VehicleEngineSizeMaxValue, ErrorMessage = "Engine size is not valid.")]
        public int EngineSize { get; set; }

        [Required]
        public DoorCount Doors { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }

        [Required]
        public VehicleCondition Condition { get; set; }

        [Url]
        [StringLength(VehicleImageUrlMaxLength, MinimumLength = VehicleImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel>? Categories { get; set; }
    }
}
