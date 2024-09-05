using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Controllers
{
    public class errorController : Controller
    {
        public IActionResult error403()
        {
            return View();
        }
        public IActionResult error404()
        {
            return View();
        }
    }
}
