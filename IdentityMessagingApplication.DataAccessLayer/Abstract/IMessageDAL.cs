using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DataAccessLayer.Abstract
{
    public interface IMessageDAL : IGenericDAL<Message>
    {
        public List<Message> GetInboxMessageList(int id);
        public List<Message> GetSentMessageList(int id);
        public List<Message> GetDraftMessageList(int id);
        public List<Message> GetJunkMessageList(int id);
        public List<Message> GetImportantMessageList(int id);
        public Message GetChangeIsReadMessageByMessageId(int id);
        public Message GetChangeIsImportantMessageByMessageId(int id);
        public Message GetChangeIsJunkMessageByMessageId(int id);
        public List<Message> GetMessageListBySenderId(int id,int id2);
        public List<Message> GetMessageListNavBarByReceiverId(int id);
        public List<Message> GetMessageListByReceiverId(int id,int id2);
        public Message GetMessageByMessageId(int id);
        public List<Message> GetAllMessageBoxDetailByUserId(int id);
        public List<Message> GetAllMessagesForDashboardCount();
        public int GetMessageCountByUserId(int id);
    }
}
