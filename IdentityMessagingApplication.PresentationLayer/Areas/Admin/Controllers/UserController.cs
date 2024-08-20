using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _appUserService = appUserService;
        }
        [Route("UserList")]
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            var user = _mapper.Map<List<ListUserDto>>(values);
            return View(user);
        }
        public IActionResult DeleteUser(int id)
        {
            _appUserService.TDelete(id);
            return RedirectToAction("UserList");
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            var values = _mapper.Map<AppUser>(createUserDto);
            _appUserService.TInsert(values);
            return RedirectToAction("UserList");
        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var value = _appUserService.TGetById(id);
            UpdateUserDto updateUserDto = new UpdateUserDto()
            {
                City = value.City,
                Email = value.Email,
                Id = id,
                Name = value.Name,
                Phone = value.Phone,
                Surname = value.Surname

            };
            return View(updateUserDto);
        }
        [HttpPost]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            var value = _mapper.Map<AppUser>(updateUserDto);
            _appUserService.TUpdate(value);
            return RedirectToAction("UserList");
        }
    }
}
