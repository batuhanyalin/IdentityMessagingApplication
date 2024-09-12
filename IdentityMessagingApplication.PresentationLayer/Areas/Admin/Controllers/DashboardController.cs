using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.EntityLayer.Concrete;
using IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
        public async Task<IActionResult> Index()
        {
            // Admin rolüne sahip kullanıcı sayısı
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            int TotalUserCount = adminUsers.Count;
            // User rolüne sahip kullanıcı sayısı
            var userUsers = await _userManager.GetUsersInRoleAsync("User");
            int TotalAdminCount = userUsers.Count;
            var TotalUserList = _appUserService.TGetUsersAllWithMessageForDashboard();
            var totalMessageList = _messageService.TGetAllMessagesForDashboardCount();
            int TotalSentboxMessageCount = totalMessageList.Count();
            int TotalInboxMessageCount = totalMessageList.Count();
            int TotalJunkboxMessageCount = totalMessageList.Where(x => x.IsJunk == true).Count();
            int TotalImportantboxMessageCount = totalMessageList.Where(x => x.IsImportant == true).Count();
            int TotalDailySentMessageCount = totalMessageList.Where(x => x.SendingTime == DateTime.Today).Count();
            int TotalDailyInboxMessageCount = totalMessageList.Where(x => x.SendingTime == DateTime.Today).Count();


            var lastestSentMessage = totalMessageList.OrderByDescending(x => x.SendingTime).FirstOrDefault();
            var todayInfo = DateTime.Now;

            if (lastestSentMessage != null)
            {
                TimeSpan timeDifference = todayInfo - lastestSentMessage.SendingTime;

                string timeAgo;
                if (timeDifference.TotalDays >= 1)
                {
                    timeAgo = $"{(int)timeDifference.TotalDays} gün önce";
                }
                else if (timeDifference.TotalHours >= 1)
                {
                    timeAgo = $"{(int)timeDifference.TotalHours} saat önce";
                }
                else if (timeDifference.TotalMinutes >= 1)
                {
                    timeAgo = $"{(int)timeDifference.TotalMinutes} dakika önce";
                }
                else
                {
                    timeAgo = $"{(int)timeDifference.TotalSeconds} saniye önce";
                }

                ViewBag.LastestSentMessageDateTime = timeAgo;
            }


            var LastRegisterAccount = TotalUserList.OrderByDescending(x => x.Id).FirstOrDefault();

            var AllUserCity = TotalUserList.OrderByDescending(x => x.City).ToList();
            var MostFromCity = AllUserCity.GroupBy(x => x.City).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();

            var receivingMessageGroups = totalMessageList.GroupBy(x => x.ReceiverId)
                                       .Select(group => new
                                       {
                                           ReceiverId = group.Key,
                                           MessageCount = group.Count()
                                       })
                                       .OrderByDescending(x => x.MessageCount)
                                       .ToList();
            var sendingMessageGroups = totalMessageList.GroupBy(x => x.SenderId)
                                     .Select(group => new
                                     {
                                         SenderId = group.Key,
                                         MessageCount = group.Count()
                                     })
                                     .OrderByDescending(x => x.MessageCount)
                                     .ToList();
            // En çok mesaj alan kullanıcı
            var MostReceivingUser = receivingMessageGroups.FirstOrDefault();
            var MostSendingUser = sendingMessageGroups.FirstOrDefault();
            var GetMostReceivingMessageUser = _appUserService.TGetById(MostReceivingUser.ReceiverId);
            var GetMostSendingMessageUser = _appUserService.TGetById(MostSendingUser.SenderId);

            var UnApprovedUsersCount = _appUserService.TGetUnApprovedUsersCount();

            var YoungestUser = _appUserService.TGetListAll().OrderByDescending(x => x.BirthDay).Take(1).FirstOrDefault();
            var OldestUser = _appUserService.TGetListAll().OrderBy(x => x.BirthDay).Take(1).FirstOrDefault();

            DashboardStatisticViewModel dashboardStatisticViewModel = new DashboardStatisticViewModel()
            {
                TotalUserCount = TotalUserCount,
                TotalAdminCount = TotalAdminCount,
                TotalSentboxMessageCount = TotalSentboxMessageCount,
                TotalInboxMessageCount = TotalInboxMessageCount,
                TotalImportantboxMessageCount = TotalImportantboxMessageCount,
                TotalJunkboxMessageCount = TotalJunkboxMessageCount,
                TotalDailyInboxMessageCount = TotalDailyInboxMessageCount,
                TotalDailySentMessageCount = TotalDailySentMessageCount,
                MostReceivingMessageUser = GetMostReceivingMessageUser,
                MostSendingMessageUser = GetMostSendingMessageUser,
                MostReceivingMessageUserCount = MostReceivingUser.MessageCount,
                MostSendingMessageUserCount = MostSendingUser.MessageCount,
                HowFromCityNameCount = MostFromCity,
                LastRegisterAccount = LastRegisterAccount,
                TotalUnApprovedUsersCount= UnApprovedUsersCount.Count(),
                OldestUser = OldestUser,
                YoungestUser = YoungestUser,
            };
            if (YoungestUser != null)
            {
                var today = DateTime.Today;
                var age = today.Year - YoungestUser.BirthDay.Value.Year;

                if (YoungestUser.BirthDay.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                ViewBag.youngestUserAge = age;
            }
            if (OldestUser != null)
            {
                var today = DateTime.Today;
                var age = today.Year - OldestUser.BirthDay.Value.Year;

                if (OldestUser.BirthDay.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                ViewBag.oldestUserAge = age;
            }
            return View(dashboardStatisticViewModel);
        }
    }
}
