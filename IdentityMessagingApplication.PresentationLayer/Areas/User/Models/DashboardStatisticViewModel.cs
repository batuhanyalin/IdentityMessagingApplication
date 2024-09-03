using IdentityMessagingApplication.EntityLayer.Concrete;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.Models
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
        public AppUser LastRegisterAccount { get; set; }
        public string HowFromCityNameCount { get; set; }
        public AppUser OldestUser { get; set; }
        public AppUser YoungestUser { get; set; }
        public AppUser MostSendingMessageUser { get; set; }
        public int MostSendingMessageUserCount {  get; set; }
        public AppUser MostReceivingMessageUser { get; set; }
        public int MostReceivingMessageUserCount { get; set; }
        public int TotalUnApprovedUsersCount { get; set; }


    }
}
