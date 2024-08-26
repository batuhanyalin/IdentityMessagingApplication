﻿using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<AppUser>
    {
        public AppUser TChangeIsApprovedUser(int id);
    }
}
