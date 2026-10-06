using CarTrader.Web.ViewModels.VehicleViewModels;
using CarTrader.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarTrader.Web.Controllers
{
    [Authorize]
    public class VehicleController : BaseController
    {
        private readonly ICarTraderService _carTraderService;


        public VehicleController(ICarTraderService carTraderService)
        {
            _carTraderService = carTraderService;
        }


        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            var vehicles = await _carTraderService.GetAllVehiclesAsync(userId);

            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _carTraderService.GetVehicleDetailsByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            vehicle.IsOwner = await _carTraderService.IsVehicleOwnerAsync(id, userId);
            vehicle.IsSaved = await _carTraderService.IsVehicleSavedAsync(id, userId);

            return View(vehicle);
        }

        //public async Task<IActionResult> MyVehicles()
        //{
        //    string? userId = GetUserId();

        //    var vehicles = await _carTraderService.GetVehiclesByUserIdAsync(userId);

        //    return View(vehicles);
        //}
    }
}
