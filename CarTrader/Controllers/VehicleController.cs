using CarTrader.Data.Models;
using CarTrader.Services.Contracts;
using CarTrader.Web.ViewModels.VehicleViewModels;
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


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            var vehicles = await _carTraderService.GetAllVehiclesAsync(userId);

            return View(vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _carTraderService.GetCreateVehicleViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = await _carTraderService.GetCreateVehicleViewModelAsync();
                return View(viewModel);
            }

            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            await _carTraderService.AddVehicleAsync(viewModel, userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyVehicles (string Id)
        {
            string? userId = GetUserId();

            if(string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            return View(await _carTraderService.GetVehiclesByUserIdAsync(userId));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var vehicle = await _carTraderService.GetVehicleForDeleteByIdAsync(id, userId);

            return View(vehicle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _carTraderService.DeleteVehicleAsync(id, userId);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction("MyVehicles");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try 
            {
                var model = await _carTraderService.GetVehicleForEditAsync(id, userId);
                return View(model);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await _carTraderService.EditVehicleAsync(viewModel, userId);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction("MyVehicles");
        }

        public async Task<IActionResult> Favorites()
        {
            string? userId = GetUserId();
            
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<FavoriteViewModel> favoriteVehicles = await _carTraderService.GetFavoriteVehiclesByUserIdAsync(userId);

            return View(favoriteVehicles);
        }

        public async Task<IActionResult> Save(int id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            await _carTraderService.SaveVehiclesAsync(id, userId);
            return RedirectToAction("Favorites");
        }

        public async Task<IActionResult> Remove(int id)
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            await _carTraderService.RemoveVehicleAsync(id, userId);
            return RedirectToAction("Favorites");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _carTraderService.GetVehicleDetailsByIdAsync(id);

            string? userId = GetUserId();
            vehicle.IsOwner = await _carTraderService.IsVehicleOwnerAsync(id, userId);

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
