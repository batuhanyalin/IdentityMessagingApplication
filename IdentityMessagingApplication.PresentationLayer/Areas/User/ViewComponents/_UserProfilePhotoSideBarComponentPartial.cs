using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.ViewComponents
{
    public class _UserProfilePhotoSideBarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _UserProfilePhotoSideBarComponentPartial(UserManager<AppUser> userManager)
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
