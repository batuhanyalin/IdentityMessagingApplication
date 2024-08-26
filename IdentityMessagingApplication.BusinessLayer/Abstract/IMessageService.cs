using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.Abstract
{
    public interface IMessageService:IGenericService<Message>
    {
        public List<Message> TGetInboxMessageList(int id);
        public List<Message> TGetSentMessageList(int id);
        public List<Message> TGetDraftMessageList(int id);
        public List<Message> TGetJunkMessageList(int id);
        public List<Message> TGetImportantMessageList(int id);
        public Message TGetChangeIsReadMessageByMessageId(int id);
        public Message TGetChangeIsImportantMessageByMessageId(int id);
        public Message TGetChangeIsJunkMessageByMessageId(int id);
        public List<Message> TGetMessageListBySenderId(int id);
        public List<Message> TGetMessageListNavBarByReceiverId(int id);
    }
}
