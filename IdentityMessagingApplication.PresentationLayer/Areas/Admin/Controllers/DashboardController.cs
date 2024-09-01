using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.EntityLayer.Concrete;
using IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public DashboardController(IMessageService messageService, UserManager<AppUser> userManager, IAppUserService userService)
        {
            _messageService = messageService;
            _userManager = userManager;
            _appUserService = userService;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            int TotalUserCount = _appUserService.TGetListAll().Count();
            int TotalAdminCount = _appUserService.TGetListAll().Count();
            var totalMessageList = _messageService.TGetAllMessagesForDashboardCount();
            int TotalSentboxMessageCount = totalMessageList.Count();
            int TotalInboxMessageCount = totalMessageList.Count();
            int TotalJunkboxMessageCount = totalMessageList.Where(x => x.IsJunk == true).Count();
            int TotalImportantboxMessageCount = totalMessageList.Where(x => x.IsImportant == true).Count();
            int TotalDailySentMessageCount = totalMessageList.Where(x => x.SendingTime == DateTime.Today).Count();
            int TotalDailyInboxMessageCount = totalMessageList.Where(x => x.SendingTime == DateTime.Today).Count();
            var MostReceivingMessage = totalMessageList.GroupBy(x => x.ReceiverId);
            var MostReceivingMessageUser =MostReceivingMessage.OrderByDescending(x=>x.Count());

            var youngestUser=_appUserService.TGetListAll().OrderByDescending(x=>x.BirthDay).Take(1).FirstOrDefault();

            DashboardStatisticViewModel dashboardStatisticViewModel = new DashboardStatisticViewModel()
            {
                TotalUserCount = TotalUserCount,
                TotalAdminCount = TotalAdminCount,
                TotalSentboxMessageCount= TotalSentboxMessageCount,
                TotalInboxMessageCount = TotalInboxMessageCount,
                TotalImportantboxMessageCount= TotalImportantboxMessageCount,
                TotalJunkboxMessageCount = TotalJunkboxMessageCount,
                TotalDailyInboxMessageCount = TotalDailyInboxMessageCount,
                TotalDailySentMessageCount= TotalDailySentMessageCount,
                MostReceivingMessageCount = MostReceivingMessage.Count(),
            };
            return View(dashboardStatisticViewModel);
        }
    }
}
