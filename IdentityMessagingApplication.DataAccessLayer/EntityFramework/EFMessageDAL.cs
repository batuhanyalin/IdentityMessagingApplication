using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Context;
using IdentityMessagingApplication.DataAccessLayer.Repositories;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DataAccessLayer.EntityFramework
{
    public class EFMessageDAL : GenericRepository<Message>, IMessageDAL
    {
        ProjectContext context = new ProjectContext();

        public List<Message> GetInboxMessageList(int id)
        {
            return context.Messages.Where(x => x.ReceiverId == id).ToList();
        }
        public List<Message> GetSentMessageList(int id)
        {
            return context.Messages.Where(x => x.SenderId == id).ToList();
        }
        public List<Message> GetDraftMessageList(int id)
        {
            return context.Messages.Where(x => x.IsDraft == true && x.SenderId == id).ToList();
        }
        public List<Message> GetJunkMessageList(int id)
        {
            return context.Messages.Where(x => x.IsJunk == true && x.ReceiverId == id).ToList();
        }
        public List<Message> GetImportantMessageList(int id)
        {
            return context.Messages.Where(x => x.IsImportant == true && x.ReceiverId == id).ToList();
        }
        public Message GetChangeIsReadMessageByMessageId(int id)
        {
            var values = context.Messages.Find(id);
            if (values.IsRead == false)
            {
                values.IsRead = true;
            }
            else
            {
                values.IsRead = true;
            }
            context.SaveChanges();
            return values;
        }
        public Message GetChangeIsImportantMessageByMessageId(int id)
        {
            var values = context.Messages.Find(id);
            if (values.IsImportant == false)
            {
                values.IsImportant = true;
            }
            else
            {
                values.IsImportant = true;
            }
            context.SaveChanges();
            return values;
        }
        public Message GetChangeIsJunkMessageByMessageId(int id)
        {
            var values = context.Messages.Find(id);
            if (values.IsJunk == false)
            {
                values.IsJunk = true;
            }
            else
            {
                values.IsJunk = true;
            }
            context.SaveChanges();
            return values;
        }
    }
}
