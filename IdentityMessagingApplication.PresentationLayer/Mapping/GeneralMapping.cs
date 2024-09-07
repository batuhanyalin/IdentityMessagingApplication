using AutoMapper;
using IdentityMessagingApplication.DtoLayer.LoginDtos;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.DtoLayer.RegisterDtos;
using IdentityMessagingApplication.DtoLayer.RoleDtos;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;

namespace IdentityMessagingApplication.PresentationLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Message, InboxMessageListDto>().ReverseMap();
            CreateMap<Message, ImportantMessageListDto>().ReverseMap();
            CreateMap<Message, JunkMessageListDto>().ReverseMap();
            CreateMap<Message, SentMessageListDto>().ReverseMap();
            CreateMap<Message, DraftMessageListDto>().ReverseMap();
            CreateMap<Message, ReadMessageDto>().ReverseMap();
            CreateMap<Message, EditDraftMessageDto>().ReverseMap();


            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, ListUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser,MyProfileUpdateDto>().ReverseMap();


            CreateMap<AppRole,CreateRoleDto>().ReverseMap();
            CreateMap<AppRole,ListRoleDto>().ReverseMap();
            CreateMap<AppRole,UpdateRoleDto>().ReverseMap();
        }
    }
}
