namespace CarTrader.Web.ViewModels.VehicleViewModels
{
    public class FavoriteViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
