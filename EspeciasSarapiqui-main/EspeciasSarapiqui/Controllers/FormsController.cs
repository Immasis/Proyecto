using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class FormsController : Controller
    {

        [Route("Forms/Basic-Elements")]
        public IActionResult Basic_Elements()
        {
            return View();
        }
        [Route("Forms/Form-Advanced")]
        public IActionResult Form_Advanced()
        {
            return View();
        }
        
        [Route("Forms/Form-Validation")]
        public IActionResult Form_Validation()
        {
            return View();
        }
        
        [Route("Forms/Form-Wizard")]
        public IActionResult Form_Wizard()
        {
            return View();
        }
        
        [Route("Forms/File-Uploads")]
        public IActionResult File_Uploads()
        {
            return View();
        } 
        
        [Route("Forms/Form-Editors")]
        public IActionResult Form_Editors()
        {
            return View();
        }
        
        [Route("Forms/Image-Crop")]
        public IActionResult Image_Crop()
        {
            return View();
        }
        
        [Route("Forms/X-Editable")]
        public IActionResult X_Editable()
        {
            return View();
        }
    }
}
