using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarTrader.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string? GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
