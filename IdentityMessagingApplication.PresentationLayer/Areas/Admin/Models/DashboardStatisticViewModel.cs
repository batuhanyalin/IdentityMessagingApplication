using IdentityMessagingApplication.EntityLayer.Concrete;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models
{
    public class DashboardStatisticViewModel
    {
        public int TotalUserCount { get; set; }
        public int TotalAdminCount { get; set; }
        public int TotalSentboxMessageCount { get; set; }
        public int TotalInboxMessageCount { get; set; }
        public int TotalJunkboxMessageCount { get; set; }
        public int TotalDraftboxMessageCount { get; set; }
        public int TotalImportantboxMessageCount { get; set; }
        public int TotalDailySentMessageCount { get; set; }
        public int TotalDailyInboxMessageCount { get; set; }
        public string LastRegisterAccount { get; set; }
        public string HowFromCityNameCount { get; set; }
        public AppUser AppUser { get; set; }
        public string OldestUser { get; set; }
        public string YoungestUser { get; set; }
        public AppUser Sender { get; set; }
        public int MostSendingMessageCount { get; set; }
        public AppUser Receiver { get; set; }
        public int MostReceivingMessageCount { get; set; }
        public string MostReceivingMessageUser { get; set; }
        public DateTime LastestSentMessageDateTime { get; set; }
        public int TotalUnApprovedUsersCount { get; set; }


    }
}
