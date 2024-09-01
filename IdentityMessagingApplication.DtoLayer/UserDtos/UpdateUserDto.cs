using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.DtoLayer.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? About { get; set; }
        public string Profession { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Message> SenderMessage { get; set; }
        public List<Message> ReceiverMessage { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
