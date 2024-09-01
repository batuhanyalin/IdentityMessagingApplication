using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDAL _messageDAL;

        public MessageManager(IMessageDAL messageDAL)
        {
            _messageDAL = messageDAL;
        }

        public void TDelete(int id)
        {
            _messageDAL.Delete(id);
        }

        public List<Message> TGetAllMessageBoxDetailByUserId(int id)
        {
            return _messageDAL.GetAllMessageBoxDetailByUserId(id);
        }

        public List<Message> TGetAllMessagesForDashboardCount()
        {
           return _messageDAL.GetAllMessagesForDashboardCount();
        }

        public Message TGetById(int id)
        {
            return _messageDAL.GetById(id);
        }

        public Message TGetChangeIsImportantMessageByMessageId(int id)
        {
            return _messageDAL.GetChangeIsImportantMessageByMessageId(id);
        }

        public Message TGetChangeIsJunkMessageByMessageId(int id)
        {
            return _messageDAL.GetChangeIsJunkMessageByMessageId(id);
        }

        public Message TGetChangeIsReadMessageByMessageId(int id)
        {
            return _messageDAL.GetChangeIsReadMessageByMessageId(id);
        }

        public List<Message> TGetDraftMessageList(int id)
        {
            return _messageDAL.GetDraftMessageList(id);
        }

        public List<Message> TGetImportantMessageList(int id)
        {
            return _messageDAL.GetImportantMessageList(id);
        }

        public List<Message> TGetInboxMessageList(int id)
        {
            return _messageDAL.GetInboxMessageList(id);
        }

        public List<Message> TGetJunkMessageList(int id)
        {
            return _messageDAL.GetJunkMessageList(id);
        }

        public List<Message> TGetListAll()
        {
            return _messageDAL.GetListAll();
        }

        public Message TGetMessageByMessageId(int id)
        {
            return _messageDAL.GetMessageByMessageId(id);
        }

        public List<Message> TGetMessageListByReceiverId(int id, int id2)
        {
            return _messageDAL.GetMessageListByReceiverId(id, id2);
        }

        public List<Message> TGetMessageListBySenderId(int id, int id2)
        {
            return _messageDAL.GetMessageListBySenderId(id, id2);
        }

        public List<Message> TGetMessageListNavBarByReceiverId(int id)
        {
            return _messageDAL.GetMessageListNavBarByReceiverId(id);
        }

        public List<Message> TGetSentMessageList(int id)
        {
            return _messageDAL.GetSentMessageList(id);
        }

        public void TInsert(Message entity)
        {
            _messageDAL.Insert(entity);
        }

        public void TUpdate(Message entity)
        {
            _messageDAL.Update(entity);
        }
    }
}
