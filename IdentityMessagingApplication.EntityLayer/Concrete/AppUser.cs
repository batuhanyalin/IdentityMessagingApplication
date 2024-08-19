﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.EntityLayer.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public List<Message> SenderMessage { get; set; }
        public List<Message> ReceiverMessage { get; set; }
    }
}
