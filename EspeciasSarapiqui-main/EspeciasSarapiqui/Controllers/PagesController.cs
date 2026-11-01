using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class PagesController : Controller
    {

        [Route("Pages/Starter-Page")]
        public IActionResult Starter_Page()
        {
            return View();
        }

        [Route("Pages/Contact-List")]
        public IActionResult Contact_List()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        } 
        
        public IActionResult Timeline()
        {
            return View();
        } 
        
        public IActionResult Invoice()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        } 
        public IActionResult Pricing()
        {
            return View();
        }
        public IActionResult Maintenance()
        {
            return View();
        }

        [Route("Pages/Error-404")]
        public IActionResult Error_404()
        {
            return View();
        }

        [Route("Pages/Error-404-alt")]
        public IActionResult Error_404_alt()
        {
            return View();
        }
        
        [Route("Pages/Error-500")]
        public IActionResult Error_500()
        {
            return View();
        }
    }
}
