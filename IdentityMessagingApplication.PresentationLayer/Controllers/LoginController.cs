using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.LoginDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityMessagingApplication.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _appUserService = appUserService;
        }
        public IActionResult ApprovedCheck()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.password, false, false);

            var user = _appUserService.TGetUserByUserName(loginDto.UserName);
            var roles = _roleManager.Roles.ToList();
            var userRole = await _userManager.GetRolesAsync(user);
            if (result.Succeeded == true && user.IsApproved == false)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("ApprovedCheck", "Login");
            }
            if (result.Succeeded)
            {
                if (userRole.FirstOrDefault()=="Admin")
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı veya parola yanlış");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        public IActionResult error403()
        {
            return View();
        }
    }
}
