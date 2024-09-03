using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.ViewComponents
{
    public class _UserLeftNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
