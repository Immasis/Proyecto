using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class ExtendedUIController : Controller
    {
        public IActionResult Portlets()
        {
            return View();
        }
        
        public IActionResult Scrollbar()
        {
            return View();
        }


        [Route("ExtendedUI/Range-Slider")]
        public IActionResult Range_Slider()
        {
            return View();
        }
        
        public IActionResult Scrollspy()
        {
            return View();
        }
    }
}
