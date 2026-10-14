using CarTrader.Data;
using CarTrader.Data.Models;
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

        public async Task<CreateViewModel> GetCreateVehicleViewModelAsync()
        {
            IEnumerable<CategoryViewModel> categories = await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
            
            CreateViewModel model = new CreateViewModel
            {
                Categories = categories
            };
            return model;
        }

        public async Task AddVehicleAsync(CreateViewModel model, string userId)
        {
            var vehicle = new Vehicle
            {
                Make = model.Make,
                Model = model.Model,
                Description = model.Description,
                Price = model.Price,
                Year = model.Year,
                Mileage = model.Mileage,
                EngineSize = model.EngineSize,
                Doors = model.Doors,
                FuelType = model.FuelType,
                TransmissionType = model.TransmissionType,
                Condition = model.Condition,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                SellerId = userId
            };
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
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

        public async Task SaveVehiclesAsync(int id, string userId)
        {

            if(await _dbContext.UserVehicles.AnyAsync(uv => uv.VehicleId == id && uv.UserId == userId))
            {
                return;
            }

            var userVehicle = new UserVehicle
            {
                VehicleId = id,
                UserId = userId
            };

            await _dbContext.UserVehicles.AddAsync(userVehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveVehicleAsync(int id, string userId)
        {
            var userVehicle = await _dbContext.UserVehicles.FirstOrDefaultAsync(uv => uv.VehicleId == id && uv.UserId == userId);

            if (userVehicle == null)
            {
                return;
            }
            _dbContext.UserVehicles.Remove(userVehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FavoriteViewModel>> GetFavoriteVehiclesByUserIdAsync(string? userId)
        {
            return await _dbContext.UserVehicles
                .Where(uv => uv.UserId == userId)
                .Include(uv => uv.Vehicle)
                .ThenInclude(v => v.Category)
                .Select(uv => new FavoriteViewModel
                {
                    Id = uv.Vehicle.Id,
                    Make = uv.Vehicle.Make,
                    Model = uv.Vehicle.Model,
                    Category = uv.Vehicle.Category.Name,
                    ImageUrl = uv.Vehicle.ImageUrl
                }).ToListAsync();
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
                Doors = vehicle.Doors.ToString(),
                FuelType = vehicle.FuelType.ToString(),
                TransmissionType = vehicle.TransmissionType.ToString(),
                Condition = vehicle.Condition.ToString(),
                CategoryName = vehicle.Category.Name,
                IsOwner = false,
                IsSaved = false
            };
        }


        public async Task<IEnumerable<UserVehicleViewModel>> GetVehiclesByUserIdAsync(string? userId)
        {
            var vehicles = await _dbContext.Vehicles
                .Where(v => v.SellerId == userId && !v.IsDeleted)
                .Include(v => v.Category)
                .Include(v => v.UserVehicles)
                .Select(v => new UserVehicleViewModel
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Price = v.Price,
                    Mileage = v.Mileage,
                    EngineSize = v.EngineSize,
                    ImageUrl = v.ImageUrl,
                    CategoryName = v.Category.Name

                }).ToListAsync();

            return vehicles;
        }


        public async Task<DeleteViewModel> GetVehicleForDeleteByIdAsync(int vehicleId, string userId)
        {
            var vehicle = _dbContext.Vehicles
                .Include(v => v.Seller)
                .FirstOrDefault(v => v.Id == vehicleId && !v.IsDeleted);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle not found.");
            }

            if (vehicle.SellerId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this vehicle.");
            }

            return new DeleteViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Price = vehicle.Price,
                Description = vehicle.Description,
                ImageUrl = vehicle.ImageUrl
            };
        }


        public async Task DeleteVehicleAsync(int vehicleId, string userId)
        {
            var vehicle = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.Id == vehicleId && !v.IsDeleted);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle not found.");
            }

            if (vehicle.SellerId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this vehicle.");
            }

            vehicle.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditViewModel> GetVehicleForEditAsync(int id, string userId)
        {
            var vehicle = await _dbContext.Vehicles
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle not found.");
            }

            if (vehicle.SellerId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this vehicle.");
            }

            var categories = await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return new EditViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Description = vehicle.Description,
                Price = vehicle.Price,
                Year = vehicle.Year,
                Mileage = vehicle.Mileage,
                EngineSize = vehicle.EngineSize,
                Doors = vehicle.Doors,
                FuelType = vehicle.FuelType,
                TransmissionType = vehicle.TransmissionType,
                Condition = vehicle.Condition,
                ImageUrl = vehicle.ImageUrl,
                CategoryId = vehicle.CategoryId,
                Categories = categories
            };
        }

        public async Task EditVehicleAsync(EditViewModel model, string userId)
        {
            var vehicle = await _dbContext.Vehicles
                .FirstOrDefaultAsync(v => v.Id == model.Id && !v.IsDeleted);

            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle not found.");
            }

            if (vehicle.SellerId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this vehicle.");
            }

            vehicle.Make = model.Make;
            vehicle.Model = model.Model;
            vehicle.Description = model.Description;
            vehicle.Price = model.Price;
            vehicle.Year = model.Year;
            vehicle.Mileage = model.Mileage;
            vehicle.EngineSize = model.EngineSize;
            vehicle.Doors = model.Doors;
            vehicle.FuelType = model.FuelType;
            vehicle.TransmissionType = model.TransmissionType;
            vehicle.Condition = model.Condition;
            vehicle.ImageUrl = model.ImageUrl;
            vehicle.CategoryId = model.CategoryId;

            await _dbContext.SaveChangesAsync();
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


        //public async Task<bool> IsVehicleSavedAsync(int vehicleId, string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return false;
        //    }

        //    return await _dbContext.UserVehicles
        //        .AnyAsync(uv => uv.VehicleId == vehicleId && uv.UserId == userId);
        //}
    }
}
