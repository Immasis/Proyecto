using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class IconsController : Controller
    {


        [Route("Icons/Remix-Icons")]
        public IActionResult Remix_Icons()
        {
            return View();
        }

        [Route("Icons/Bootstrap-Icons")]
        public IActionResult Bootstrap_Icons()
        {
            return View();
        }

        [Route("Icons/Material-Design-Icons")]
        public IActionResult Material_Design_Icons()
        {
            return View();
        }
    }
}
