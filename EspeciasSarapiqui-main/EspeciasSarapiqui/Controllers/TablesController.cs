using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class TablesController : Controller
    {

        [Route("Tables/Basic-Tables")]
        public IActionResult Basic_Tables()
        {
            return View();
        }

        [Route("Tables/Data-Tables")]
        public IActionResult Data_Tables()
        {
            return View();
        }
        
        [Route("Tables/Editable-Tables")]
        public IActionResult Editable_Tables()
        {
            return View();
        }
        
        [Route("Tables/Responsive-Table")]
        public IActionResult Responsive_Table()
        {
            return View();
        }
    }
}
