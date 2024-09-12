using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<AppUser>
    {
        public List<AppUser> TGetUsersAllWithMessage();
        public List<AppUser> TGetUsersAllWithMessageForDashboard();
        public List<AppUser> TGetUnApprovedUsersCount();
        public AppUser TGetUserByUserName(string UserName);

	}
}
