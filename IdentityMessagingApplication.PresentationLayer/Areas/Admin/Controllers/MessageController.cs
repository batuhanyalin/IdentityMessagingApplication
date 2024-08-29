using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessagingApplication.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Route("InboxMessageList")]
        public async Task<IActionResult> InboxMessageList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetInboxMessageList(user.Id);
            var values = _mapper.Map<List<InboxMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        [Route("SentMessageList")]
        public async Task<IActionResult> SentMessageList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetSentMessageList(user.Id);
            var values = _mapper.Map<List<SentMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        [Route("DraftMessageList")]
        public async Task<IActionResult> DraftMessageList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetDraftMessageList(user.Id);
            var values = _mapper.Map<List<DraftMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        [Route("JunkMessageList")]
        public async Task<IActionResult> JunkMessageList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetJunkMessageList(user.Id);
            var values = _mapper.Map<List<JunkMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        [Route("ImportantMessageList")]
        public async Task<IActionResult> ImportantMessageList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetImportantMessageList(user.Id);
            var values = _mapper.Map<List<ImportantMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        public async Task<IActionResult> ChangeIsReadMessage(int id)
        {
            var value = _messageService.TGetChangeIsReadMessageByMessageId(id);
            return RedirectToAction("InboxMessageList");
        }
        public async Task<IActionResult> ChangeIsImportantMessage(int id)
        {
            var value = _messageService.TGetChangeIsImportantMessageByMessageId(id);
            return RedirectToAction("InboxMessageList");
        }
        public async Task<IActionResult> ChangeIsJunkMessage(int id)
        {
            var value = _messageService.TGetChangeIsJunkMessageByMessageId(id);
            return RedirectToAction("InboxMessageList");
        }

        [Route("GetMessageListBySenderId/{id:int}")]
        [HttpGet]
        public IActionResult GetMessageListBySenderId(int id)
        {
            var value = _messageService.TGetMessageListBySenderId(id);
            var values=_mapper.Map<List<InboxMessageListDto>>(value);
            return View(values);
        }
    }
}
