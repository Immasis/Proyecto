using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
