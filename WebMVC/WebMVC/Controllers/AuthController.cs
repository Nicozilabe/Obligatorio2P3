using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }
}
