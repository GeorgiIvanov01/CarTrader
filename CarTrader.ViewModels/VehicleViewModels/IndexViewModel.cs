namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public double Price { get; set; }

        public int Mileage { get; set; }

        public int EngineSize { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsOwner { get; set; }

        public bool IsSaved { get; set; }
    }
}
