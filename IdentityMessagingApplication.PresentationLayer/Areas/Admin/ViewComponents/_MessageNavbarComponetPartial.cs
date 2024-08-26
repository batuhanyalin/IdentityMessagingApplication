using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.ViewComponents
{
    public class _MessageNavbarComponetPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public _MessageNavbarComponetPartial(IMessageService messageService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _messageService = messageService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetMessageListNavBarByReceiverId(user.Id);
            var values=_mapper.Map<List<InboxMessageListDto>>(value);
            return View(values);
        }
    }
}
