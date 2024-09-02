using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Context;
using IdentityMessagingApplication.DataAccessLayer.Repositories;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DataAccessLayer.EntityFramework
{
    public class EFAppUserDAL : GenericRepository<AppUser>, IAppUserDAL
    {
        ProjectContext context = new ProjectContext();
        public AppUser ChangeIsApprovedUser(int id)
        {
            var values = context.Users.Find(id);
            if (values.IsApproved == false)
            {
                values.IsApproved = true;
            }
            else
            {
                values.IsApproved = false;
            }
            context.SaveChanges();
            return values;
        }
        public List<AppUser> GetUsersAllWithMessage()
        {
            var values= context.Users.Include(x=>x.SenderMessage).Include(x=>x.ReceiverMessage).ToList();
            return values;
        }
        public List<AppUser> GetUnApprovedUsersCount()
        {
            var values = context.Users.Where(x=>x.IsApproved==false).ToList();
            return values;
        }
    }
}
