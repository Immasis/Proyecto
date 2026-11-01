using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class LayoutsController : Controller
    {
        public IActionResult Horizontal()
        {
            return View();
        }


        [Route("Layouts/Light-Sidebar")]
        public IActionResult Light_Sidebar()
        {
            return View();
        }

        [Route("Layouts/Small-Sidebar")]
        public IActionResult Small_Sidebar()
        {
            return View();
        }


        [Route("Layouts/Collapsed-Sidebar")]
        public IActionResult Collapsed_Sidebar()
        {
            return View();
        }


        [Route("Layouts/Unsticky-Layout")]
        public IActionResult Unsticky_Layout()
        {
            return View();
        }


        [Route("Layouts/Boxed-Layout")]
        public IActionResult Boxed_Layout()

        {
            return View();
        }
    }
}
