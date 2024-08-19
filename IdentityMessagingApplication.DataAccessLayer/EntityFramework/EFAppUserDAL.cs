﻿using IdentityMessagingApplication.DataAccessLayer.Abstract;
using IdentityMessagingApplication.DataAccessLayer.Repositories;
using IdentityMessagingApplication.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DataAccessLayer.EntityFramework
{
    public class EFAppUserDAL:GenericRepository<AppUser>,IAppUserDAL
    {
    }
}
