using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DataAccessLayer.Abstract
{
    public interface IAppUserDAL:IGenericDAL<AppUser>
    {
        public AppUser ChangeIsApprovedUser(int id);
        public List<AppUser> GetUsersAllWithMessage();
        public List<AppUser> GetUsersAllWithMessageForDashboard();
        public List<AppUser> GetUnApprovedUsersCount();
        public AppUser GetUserByUserName(string UserName);

	}
}
