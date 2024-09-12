using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.RoleDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	[Route("Admin/[controller]")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = _roleManager.Roles.ToList();
            var roles = _mapper.Map<List<ListRoleDto>>(values);
            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                role.UserCount = usersInRole.Count;
            }
            return View(roles);
        }
        [Route("JSUpdateRole")]
        public async Task<IActionResult> JSUpdateRole(UpdateRoleDto updateRoleDto)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleDto.Id);
            if (value == null)
            {
                return Json(new { success = false, messsage = "Rol bulunamadı." });
            }
            value.Name = updateRoleDto.Name;
            var result = await _roleManager.UpdateAsync(value);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Rol başarıyla güncellendi!" });
            }
            else
            {
                return Json(new { success = false, message = "Güncelleme sırasında bir hata oluştu." });
            }
        }
        [Route("JSCreateRole")]
        public async Task<IActionResult> JSCreateRole(CreateRoleDto createRoleDto)
        {
            if (createRoleDto.Name == null)
            {
                return Json(new { success = false, message = "Rol adı boş geçilemez!" });
            }
            AppRole appRole = new AppRole()
            {
                Name = createRoleDto.Name,
            };
            var result = await _roleManager.CreateAsync(appRole);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Rol başarıyla kaydedildi." });
            }
            else
            {
                return Json(new { success = false, message = "Rol kaydetme işlemi sırasında bir hata oluştu" });
            }
        }
        [Route("GetRole/{id:int}")]
        public ActionResult GetRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var role = _mapper.Map<ListRoleDto>(value);
            var jsonRole = JsonConvert.SerializeObject(role);
            return Json(jsonRole);
        }
        [Route("DeleteRole/{id:int}")]
        public IActionResult DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            _roleManager.DeleteAsync(value);
            return Json(new { success = true });
        }

    }
}
