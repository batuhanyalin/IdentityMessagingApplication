using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.EntityLayer.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? About { get; set; }
        public string? ImageUrl {  get; set; }
        public string Phone { get; set; }
        public string Profession { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public List<Message> SenderMessage { get; set; }
        public List<Message> ReceiverMessage { get; set; }
    }
}
