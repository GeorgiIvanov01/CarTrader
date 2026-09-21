namespace CarTrader.GCommon
{
    public class ValidationConstants
    {
        //Vehicle validation constants
        public const int VehicleMakeMaxLength = 200;
        public const int VehicleMakeMinLength = 2;

        public const int VehicleModelMaxLength = 200;
        public const int VehicleModelMinLength = 2;

        public const int VehicleDescriptionMaxLength = 500;
        public const int VehicleDescriptionMinLength = 2;

        public const int VehicleImageUrlMaxLength = 500;
        public const int VehicleImageUrlMinLength = 2;

        public const int VehicleYearMaxValue = 2026;
        public const int VehicleYearMinValue = 1886;

        public const int VehicleMileageMaxValue = 1_000_000;
        public const int VehicleMileageMinValue = 0;

        public const int VehicleEngineSizeMaxValue = 3000;
        public const int VehicleEngineSizeMinValue = 0;


        //Category validation constants
        public const int CategoryNameMaxLength = 100;
    }
}
