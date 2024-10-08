﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	[Route("Admin/[controller]")]
    public class AdminLayoutController : Controller
    {     
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
