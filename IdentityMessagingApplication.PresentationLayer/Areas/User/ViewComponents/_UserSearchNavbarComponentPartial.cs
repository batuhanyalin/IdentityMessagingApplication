using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.ViewComponents
{
    public class _UserSearchNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
