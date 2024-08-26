using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost]
        [Route("JSUpdateUser")]
        public async Task<IActionResult> JSUpdateUser(UpdateUserDto updateUserDto, IFormFile Image)
        {
            // Güncellenmek istenen kullanıcının mevcut bilgilerini al
            var currentUser = await _userManager.FindByIdAsync(updateUserDto.Id.ToString());

            if (currentUser == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            // Yeni kullanıcı adının başka bir kullanıcıya ait olup olmadığını kontrol et
            var search = await _userManager.FindByNameAsync(updateUserDto.UserName);
            if (search != null && search.Id != currentUser.Id)
            {
                // Kullanıcı adı başka bir kullanıcı tarafından kullanılıyor
                return Json(new { success = false, message = "Kullanıcı adı zaten sistemde kayıtlı." });
            }

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
                updateUserDto.ImageUrl = $"/images/users/{imageName}";
            }
            else if (updateUserDto.ImageUrl == null)
            {
                updateUserDto.ImageUrl = $"/images/no-image.jpg";
            }

            currentUser.UserName = updateUserDto.UserName;
            currentUser.Name = updateUserDto.Name;
            currentUser.Email = updateUserDto.Email;
            currentUser.Surname = updateUserDto.Surname;
            currentUser.Profession = updateUserDto.Profession;
            currentUser.Phone = updateUserDto.Phone;
            currentUser.About = updateUserDto.About;
            currentUser.City = updateUserDto.City;
            currentUser.ImageUrl = updateUserDto.ImageUrl;

            // Kullanıcıyı güncelle
            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Kullanıcı başarıyla güncellendi!" });
            }
            else
            {
                return Json(new { success = false, message = "Güncelleme sırasında bir hata oluştu." });
            }
        }

        [Route("UserList")]
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            var users = _mapper.Map<List<ListUserDto>>(values);
            return View(users);
        }

        [Route("JSUserList")]
        public IActionResult JSUserList()
        {
            var values = _userManager.Users.ToList();
            var user = _mapper.Map<List<ListUserDto>>(values);
            var jsonUsers = JsonConvert.SerializeObject(user);
            return Json(jsonUsers);
        }

        [Route("DeleteUser/{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            _appUserService.TDelete(id);
            return Json(new { success = true });

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


        [Route("GetUser/{id:int}")]
        public IActionResult GetUser(int id)
        {
            var value = _appUserService.TGetById(id);
            var user = _mapper.Map<ListUserDto>(value);
            var jsonUser = JsonConvert.SerializeObject(user);
            return Json(jsonUser);
        }

        [HttpGet]
        [Route("UpdateUser/{id:int}")]
        public IActionResult UpdateUser(int id)
        {
            var value = _appUserService.TGetById(id);
            UpdateUserDto updateUserDto = new UpdateUserDto()
            {
                City = value.City,
                Email = value.Email,
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

        [Route("ChangeIsApprovedUser/{id:int}")]
        public IActionResult ChangeIsApprovedUser(int id)
        {
            _appUserService.TChangeIsApprovedUser(id);
            return RedirectToAction("UserList");
        }
    }
}
