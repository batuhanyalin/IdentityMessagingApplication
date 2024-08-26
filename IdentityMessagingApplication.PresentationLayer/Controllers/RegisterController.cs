using AutoMapper;
using Humanizer;
using IdentityMessagingApplication.BusinessLayer.ValidationRules.UserValidation;
using IdentityMessagingApplication.DtoLayer.RegisterDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            var validation = new RegisterUserValidation().Validate(registerDto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(registerDto);
            }
            if (ModelState.IsValid)
            {
                var value = _mapper.Map<AppUser>(registerDto);
                value.IsApproved = false;
                if (value.ImageUrl == null)
                {
                    value.ImageUrl = $"/images/no-image.jpg";
                }
                var result = await _userManager.CreateAsync(value, registerDto.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
