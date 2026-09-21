namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class VehicleCardViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public int EngineSize { get; set; }

        public string? ImageUrl { get; set; }

    }
}
