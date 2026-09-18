namespace CarTrader.GCommon
{
    public class ValidationConstants
    {
        //Vehicle validation constants
        public const int VehicleMakeMaxLength = 200;
        public const int VehicleModelMaxLength = 200;

        public const int VehicleDescriptionMaxLength = 500;

        public const int VehicleImageUrlMaxLength = 500;

        public const int VehicleYearMinValue = 1886;
        public const int VehicleYearMaxValue = 2026; 

        public const int VehicleMileageMinValue = 0;

        public const int VehicleEngineSizeMinValue = 0;
        public const int VehicleEngineSizeMaxValue = 3000;


        //Category validation constants
        public const int CategoryNameMaxLength = 100;
    }
}
