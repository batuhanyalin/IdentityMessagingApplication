using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.ViewComponents
{
    public class _ProfilePhotoSideBarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _ProfilePhotoSideBarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userPhoto=user.ImageUrl;
            ViewBag.userName=user.Name +" "+user.Surname;
            return View();
        }
    }
}
