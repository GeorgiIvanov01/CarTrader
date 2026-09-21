namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Mileage { get; set; }

        public int EngineSize { get; set; }

        public string Doors { get; set; } = null!;

        public string FuelType { get; set; } = null!;

        public string TransmissionType { get; set; } = null!;

        public string Condition { get; set; } = null!;

        public string CategoryName { get; set; } = null!;
    }
}
