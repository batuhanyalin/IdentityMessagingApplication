using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]")]
    public class UserLayoutController : Controller
    {     
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
