using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Web.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
