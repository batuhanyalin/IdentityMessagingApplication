using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.BusinessLayer.ValidationRules.UserValidation;
using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Context;
using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.Concrete
{
    public class AppRoleManager : IAppRoleService
    {
        private readonly IAppRoleDAL _appRoleDAL;

        public AppRoleManager(IAppRoleDAL appRoleDAL)
        {
            _appRoleDAL = appRoleDAL;
        }

        public void TDelete(int id)
        {
           _appRoleDAL.Delete(id);
        }

        public AppRole TGetById(int id)
        {
          return _appRoleDAL.GetById(id);
        }

        public List<AppRole> TGetListAll()
        {
            return _appRoleDAL.GetListAll();
        }

        public void TInsert(AppRole entity)
        {
          _appRoleDAL.Insert(entity);
        }

        public void TUpdate(AppRole entity)
        {
            _appRoleDAL.Update(entity); 
        }
    }
}
