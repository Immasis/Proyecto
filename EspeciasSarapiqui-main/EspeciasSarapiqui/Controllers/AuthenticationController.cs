using Microsoft.AspNetCore.Mvc;

namespace Velonic.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        } 
        
        public IActionResult Logout()
        {
            return View();
        }

        [Route("Authentication/Forgot-Password")]
        public IActionResult Forgot_Password()
        {
            return View();
        }
        
        [Route("Authentication/Lock-Screen")]
        public IActionResult Lock_Screen()
        {
            return View();
        }
    }
}
