using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.ViewComponents
{
    public class _UserScriptsComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
