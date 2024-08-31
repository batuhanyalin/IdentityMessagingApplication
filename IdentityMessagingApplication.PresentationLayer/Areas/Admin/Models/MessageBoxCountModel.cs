namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models
{
    public class MessageBoxCountModel
    {
        public int InboxCount { get; set; }
        public int SentboxCount { get; set; }
        public int JunkboxCount { get; set; }
        public int ImportantboxCount { get; set; }
        public int DraftboxCount { get; set; }
    }
}
