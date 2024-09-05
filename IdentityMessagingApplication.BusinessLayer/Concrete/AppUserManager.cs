using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using System.Threading.Tasks;
using IdentityMessagingApplication.EntityLayer.Concrete;
using IdentityMessagingApplication.DataAccessLayer.Context;
using System.Runtime.CompilerServices;
using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.UserDtos;

namespace IdentityMessagingApplication.BusinessLayer.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDAL _appUserDAL;

        public AppUserManager(IAppUserDAL appUserDAL)
        {
            _appUserDAL = appUserDAL;
        }

        public AppUser TChangeIsApprovedUser(int id)
        {
            return _appUserDAL.ChangeIsApprovedUser(id);
        }

        public void TDelete(int id)
        {
            _appUserDAL.Delete(id);
        }

        public AppUser TGetById(int id)
        {
            return _appUserDAL.GetById(id);
        }

        public List<AppUser> TGetListAll()
        {
            return _appUserDAL.GetListAll();
        }

        public List<AppUser> TGetUnApprovedUsersCount()
        {
           return _appUserDAL.GetUnApprovedUsersCount();
        }

		public AppUser TGetUserByUserName(string UserName)
		{
			return _appUserDAL.GetUserByUserName(UserName);
		}

		public List<AppUser> TGetUsersAllWithMessage()
        {
            return _appUserDAL.GetUsersAllWithMessage();
        }

        public List<AppUser> TGetUsersAllWithMessageForDashboard()
        {
            return _appUserDAL.GetUsersAllWithMessageForDashboard();
        }

        public void TInsert(AppUser entity)
        {
            _appUserDAL.Insert(entity);
        }

        public void TUpdate(AppUser entity)
        {
            _appUserDAL.Update(entity);
        }
    }
}
