using CarTrader.Data;
using CarTrader.Services.Contracts;
using CarTrader.Web.ViewModels.VehicleViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services
{
    public class CarTraderService : ICarTraderService
    {
        private readonly CarTraderDbContext _dbContext;

        public CarTraderService(CarTraderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> GetAllVehiclesAsync(string? userId)
        {
            var vehicles = await _dbContext.Vehicles
                .Where(v => !v.IsDeleted)
                .Include(v => v.Category)
                .Select(v => new IndexViewModel
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Price = v.Price,
                    Mileage = v.Mileage,
                    EngineSize = v.EngineSize,
                    ImageUrl = v.ImageUrl,
                    CategoryName = v.Category.Name,
                    IsOwner = userId != null && v.SellerId == userId,
                    IsSaved = userId != null && v.UserVehicles.Any(uv => uv.VehicleId == v.Id && uv.UserId == userId)
                }).ToListAsync();

            return vehicles;
        }

        public async Task<DetailsViewModel> GetVehicleDetailsByIdAsync(int vehicleId)
        {
            var vehicle = await _dbContext.Vehicles
                .Include(v => v.Category)
                .Include(v => v.Seller)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with ID {vehicleId} not found.");
            }

            return new DetailsViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Price = vehicle.Price,
                Description = vehicle.Description,
                ImageUrl = vehicle.ImageUrl,
                Mileage = vehicle.Mileage,
                EngineSize = vehicle.EngineSize,
                Doors = vehicle.Doors.ToString(),// Assuming Doors is an integer, convert it to string
                FuelType = vehicle.FuelType.ToString(),// Assuming FuelType is an enum, convert it to string
                TransmissionType = vehicle.TransmissionType.ToString(),// Assuming TransmissionType is an enum, convert it to string
                Condition = vehicle.Condition.ToString(),// Assuming Condition is an enum, convert it to string
                CategoryName = vehicle.Category.Name,
                IsOwner = false, // This will be set in the controller based on the current user
                IsSaved = false // This will be set in the controller based on the current user
            };
        }

        public async Task<bool> IsVehicleOwnerAsync(int vehicleId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return await _dbContext.Vehicles
                .AnyAsync(v => v.Id == vehicleId && v.SellerId == userId);
        }

        public async Task<bool> IsVehicleSavedAsync(int vehicleId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return await _dbContext.UserVehicles
                .AnyAsync(uv => uv.VehicleId == vehicleId && uv.UserId == userId);
        }

        // Not in use, delete if not needed!
        //public async Task<IEnumerable<UserVehicleViewModel>> GetVehiclesByUserIdAsync(string? userId)
        //{

        //    var vehicles = await _dbContext.Vehicles
        //        .Where(v => v.SellerId == userId && !v.IsDeleted)
        //        .Include(v => v.Category)
        //        .Include(v => v.UserVehicles)
        //        .Select(v => new UserVehicleViewModel
        //        {
        //            Id = v.Id,
        //            Make = v.Make,
        //            Model = v.Model,
        //            Year = v.Year,
        //            Price = v.Price,
        //            Mileage = v.Mileage,
        //            EngineSize = v.EngineSize,
        //            ImageUrl = v.ImageUrl,
        //            CategoryName = v.Category.Name

        //        }).ToListAsync();

        //    return vehicles;
        //}
    }
}
