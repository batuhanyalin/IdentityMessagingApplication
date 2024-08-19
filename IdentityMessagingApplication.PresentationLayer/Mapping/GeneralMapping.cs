using AutoMapper;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
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
        }
    }
}
