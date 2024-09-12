using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Context;
using IdentityMessagingApplication.DataAccessLayer.Repositories;
using IdentityMessagingApplication.DtoLayer.UserDtos;
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

        public List<AppUser> GetUsersAllWithMessage()
        {
            var values= context.Users.Include(x=>x.SenderMessage).Include(x=>x.ReceiverMessage).ToList();
            return values;
        }
        public List<AppUser> GetUsersAllWithMessageForDashboard()
        {
            var values = context.Users.Include(x => x.SenderMessage).Include(x => x.ReceiverMessage).ToList();
            return values;
        }
        public List<AppUser> GetUnApprovedUsersCount()
        {
            var values = context.Users.Where(x=>x.IsApproved==false).ToList();
            return values;
        }
        public AppUser GetUserByUserName(string UserName)
        {
            var values = context.Users.Where(x=>x.UserName==UserName).FirstOrDefault();
            return values;
        }

	}
}
