using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarTrader.Web.ViewModels.VehicleViewModels;

namespace CarTrader.Services.Contracts
{
    public interface ICarTraderService
    {
        Task<IEnumerable<IndexViewModel>> GetAllVehiclesAsync(string? userId);

        Task<DetailsViewModel> GetVehicleDetailsByIdAsync(int vehicleId);

        Task <CreateViewModel> GetCreateVehicleViewModelAsync();

        Task AddVehicleAsync(CreateViewModel model, string userId);

        Task SaveVehiclesAsync(int id, string userId);

        Task RemoveVehicleAsync(int id, string userId);

        Task<IEnumerable<FavoriteViewModel>> GetFavoriteVehiclesByUserIdAsync(string? userId);
        Task<bool> IsVehicleOwnerAsync(int vehicleId, string userId);

        //Task<bool> IsVehicleSavedAsync(int vehicleId, string userId);

        //Task<IEnumerable<UserVehicleViewModel>> GetVehiclesByUserIdAsync(string? userId);
    }
}
