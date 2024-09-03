using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.BusinessLayer.ValidationRules.UserValidation;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.DtoLayer.RegisterDtos;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using IdentityMessagingApplication.PresentationLayer.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _messageService = messageService;
        }

        [Route("MyProfile")]
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            var value = _mapper.Map<MyProfileUpdateDto>(user);
            return View(value);
        }
        [HttpPost]
        [Route("MyProfile")]
        public async Task<IActionResult> MyProfile(MyProfileUpdateDto myProfileUpdateDto, IFormFile Image)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var validation = new MyProfileEditValidation().Validate(myProfileUpdateDto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(myProfileUpdateDto);
            }
            else
            {
                if (Image != null && Image.Length > 0)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = Path.Combine(resource, "wwwroot/images/users/", imageName);
                    using (var stream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    myProfileUpdateDto.ImageUrl = $"/images/users/{imageName}";
                    user.ImageUrl = myProfileUpdateDto.ImageUrl;
                }
                else if (user.ImageUrl == null)
                {
                    user.ImageUrl = $"/images/no-image.jpg";
                }

                user.Name = myProfileUpdateDto.Name;
                user.Surname = myProfileUpdateDto.Surname;
                user.PhoneNumber = myProfileUpdateDto.Phone;
                user.About = myProfileUpdateDto.About;
                user.Email = myProfileUpdateDto.Email;
                user.City = myProfileUpdateDto.City;
                user.Profession = myProfileUpdateDto.Profession;
                user.BirthDay = myProfileUpdateDto.BirthDay;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Güncelleme işlemi başarıyla gerçekleşti." });
                }
                else
                {
                    return Json(new { success = false, message = "Güncelleme işlemi başarısız oldu." });
                }
            }

        }
        [HttpPost]
        [Route("MyProfileChangePassword")]
        public async Task<IActionResult> MyProfileChangePassword(MyProfileUpdateDto myProfileUpdateDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkPassword = await _userManager.CheckPasswordAsync(user, myProfileUpdateDto.OldPassword);
            if (!checkPassword)
            {
                return Json(new { success = false, message = "Güncel parolanızı yanlış girdiniz. Lütfen tekrar deneyin." });
            }
            if (myProfileUpdateDto.NewPassword != myProfileUpdateDto.ConfirmPassword)
            {
                return Json(new { success = false, message = "Girilen yeni şifreler birbirleriyle eşleşmiyor. Lütfen tekrar girin." });
            }

            else
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, myProfileUpdateDto.ConfirmPassword);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Parola başarıyla güncellendi!" });
                }
                else
                {
                    return Json(new { success = false, message = "Parola güncelleme sırasında bir hata oluştu." });
                }
            }
        }
    }
}
