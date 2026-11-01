using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class MapsController : Controller
    {

        [Route("Maps/Google-Maps")]
        public IActionResult Google_Maps()
        {
            return View();
        }
        
        [Route("Maps/Vector-Maps")]
        public IActionResult Vector_Maps()
        {
            return View();
        }
    }
}
