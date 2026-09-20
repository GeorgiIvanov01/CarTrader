using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Web.Controllers
{
    [Authorize]
    public class VehicleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
