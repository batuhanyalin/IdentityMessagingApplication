using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.BusinessLayer.ValidationRules.UserValidation;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.DtoLayer.RegisterDtos;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _messageService = messageService;
        }

        [Route("GetUser/{id:int}")]
        public IActionResult GetUser(int id)
        {
            var value = _appUserService.TGetById(id);
            var user = _mapper.Map<ListUserDto>(value);
            var jsonUser = JsonConvert.SerializeObject(user);
            return Json(jsonUser);
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
                currentUser.ImageUrl = updateUserDto.ImageUrl;
            }

            currentUser.UserName = updateUserDto.UserName;
            currentUser.Name = updateUserDto.Name;
            currentUser.Email = updateUserDto.Email;
            currentUser.Surname = updateUserDto.Surname;
            currentUser.Profession = updateUserDto.Profession;
            currentUser.Phone = updateUserDto.Phone;
            currentUser.About = updateUserDto.About;
            currentUser.City = updateUserDto.City;
            currentUser.BirthDay = updateUserDto.BirthDay;

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
            var values = _appUserService.TGetUsersAllWithMessage();
            var users = _mapper.Map<List<ListUserDto>>(values);
            return View(users);
        }

        [Route("JSUserList")]
        public IActionResult JSUserList()
        {
            var values = _appUserService.TGetUsersAllWithMessage();
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


        [Route("GetUserDetail/{id:int}")]
        public IActionResult GetUserDetail(int id)
        {
            int InboxCount = _messageService.TGetInboxMessageList(id).Count();
            int SentboxCount = _messageService.TGetSentMessageList(id).Count();
            int DraftboxCount = _messageService.TGetDraftMessageList(id).Count();
            int JunkboxCount = _messageService.TGetJunkMessageList(id).Count();
            int ImportantboxCount = _messageService.TGetImportantMessageList(id).Count();
            MessageBoxCountModel messageBoxCountModel = new MessageBoxCountModel()
            {
                InboxCount = InboxCount,
                SentboxCount = SentboxCount,
                DraftboxCount = DraftboxCount,
                JunkboxCount = JunkboxCount,
                ImportantboxCount = ImportantboxCount,
            };
            var jsonUser = JsonConvert.SerializeObject(messageBoxCountModel);
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
