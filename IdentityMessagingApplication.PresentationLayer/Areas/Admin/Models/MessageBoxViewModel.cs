using IdentityMessagingApplication.DtoLayer.MessageDtos;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Models
{
    public class MessageBoxViewModel
    {
        public int InboxMessageCount { get; set; }
        public int SentMessageCount { get; set; }
        public int DraftMessageCount { get; set; }
        public int JunkMessageCount { get; set; }
        public int ImportantMessageCount { get; set; }
        public List<InboxMessageListDto> Messages { get; set; }
        public string SearchQuery { get; set; }
    }
}
