using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DtoLayer.MessageDtos
{
    public class SentMessageListDto
    {
        public int MessageId { get; set; }
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SendingTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsJunk { get; set; }
        public bool IsImportant { get; set; }
    }
}
