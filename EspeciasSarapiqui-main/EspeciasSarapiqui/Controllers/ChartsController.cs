using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class ChartsController : Controller
    {

        [Route("Charts/Apex-Charts")]
        public IActionResult Apex_Charts()
        {
            return View();
        }
        
        public IActionResult Chartjs()
        {
            return View();
        }
        [Route("Charts/Sparkline-Charts")]
        public IActionResult Sparkline_Charts()
        {
            return View();
        }
    }
}
