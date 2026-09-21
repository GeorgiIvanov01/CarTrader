namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class DeleteViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
