using AutoMapper;
using IdentityMessagingApplication.BusinessLayer.Abstract;
using IdentityMessagingApplication.DtoLayer.MessageDtos;
using IdentityMessagingApplication.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityMessagingApplication.PresentationLayer.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    [Route("User/[controller]")]
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
        [HttpPost]
        [Route("EditDraftMessage")]
        public async Task<IActionResult> EditDraftMessage(EditDraftMessageDto editDraftMessageDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            editDraftMessageDto.SenderId = user.Id;
            editDraftMessageDto.SendingTime = DateTime.Now;
            editDraftMessageDto.IsDraft = true;
            Message message = new Message()
            {
                IsImportant = editDraftMessageDto.IsImportant,
                IsRead = editDraftMessageDto.IsRead,
                IsJunk = editDraftMessageDto.IsJunk,
                IsDraft = editDraftMessageDto.IsDraft,
                ReceiverId = editDraftMessageDto.ReceiverId,
                SenderId = editDraftMessageDto.SenderId,
                Content = editDraftMessageDto.Content,
                Subject = editDraftMessageDto.Subject,
                ReadingTime = editDraftMessageDto.ReadingTime,
                SendingTime = editDraftMessageDto.SendingTime,
                MessageId = editDraftMessageDto.MessageId,
            };

            if (message.ReceiverId == message.SenderId)
            {
                return Json(new { success = false, message = "Kendinize mesaj gönderemezsiniz, lütfen başka bir alıcı seçin." });
            }
            _messageService.TUpdate(message);
            return Json(new { success = true });
        }


        [HttpGet]
        [Route("EditDraftMessage/{id:int}")]
        public async Task<IActionResult> EditDraftMessage(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mesaj = _messageService.TGetById(id);
            var mapMessage = _mapper.Map<EditDraftMessageDto>(mesaj);
            var users = _userManager.Users.ToList();
            List<SelectListItem> userList = (from x in users.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = $"{x.UserName} - {x.Name} {x.Surname} - {x.Email}",
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.userList = userList;
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(mapMessage);
        }


        [HttpPost]
        [Route("DeleteMessageFromDraft")]
        public IActionResult DeleteMessageFromDraft([FromBody] List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                return Json(new { success = false, message = "Geçersiz mesaj ID'leri" });
            }

            foreach (var messageId in messageIds)
            {
                var message = _messageService.TGetById(messageId);
                if (message != null)
                {
                    _messageService.TDelete(message.MessageId);
                }
            }
            return Json(new { success = true, message = "Mesajlar veritabanından kalıcı olarak silindi." });
        }

        [HttpPost]
        [Route("DraftMessage")]
        public async Task<IActionResult> DraftMessage(CreateMessageDto createMessageDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createMessageDto.SenderId = user.Id;
            createMessageDto.SendingTime = DateTime.Now;
            createMessageDto.IsDraft = true;
            Message message = new Message()
            {
                IsImportant = createMessageDto.IsImportant,
                IsRead = createMessageDto.IsRead,
                IsJunk = createMessageDto.IsJunk,
                IsDraft = createMessageDto.IsDraft,
                ReceiverId = createMessageDto.ReceiverId,
                SenderId = createMessageDto.SenderId,
                Content = createMessageDto.Content,
                Subject = createMessageDto.Subject,
                ReadingTime = createMessageDto.ReadingTime,
                SendingTime = createMessageDto.SendingTime,
                MessageId = createMessageDto.MessageId,
            };

            if (message.ReceiverId == message.SenderId)
            {
                return Json(new { success = false, message = "Kendinize mesaj gönderemezsiniz, lütfen başka bir alıcı seçin." });
            }
            _messageService.TInsert(message);
            return Json(new { success = true });
        }
        [HttpPost]
        [Route("EditDraftCreateMessage")]
        public async Task<IActionResult> EditDraftCreateMessage(EditDraftMessageDto editDraftMessageDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            editDraftMessageDto.SenderId = user.Id;
            editDraftMessageDto.SendingTime = DateTime.Now;
            editDraftMessageDto.IsDraft = false;
            Message message = new Message()
            {
                IsImportant = editDraftMessageDto.IsImportant,
                IsRead = editDraftMessageDto.IsRead,
                IsJunk = editDraftMessageDto.IsJunk,
                IsDraft = editDraftMessageDto.IsDraft,
                ReceiverId = editDraftMessageDto.ReceiverId,
                SenderId = editDraftMessageDto.SenderId,
                Content = editDraftMessageDto.Content,
                Subject = editDraftMessageDto.Subject,
                ReadingTime = editDraftMessageDto.ReadingTime,
                SendingTime = editDraftMessageDto.SendingTime,
                MessageId = editDraftMessageDto.MessageId,
            };
            if (message.ReceiverId == message.SenderId)
            {
                return Json(new { success = false, message = "Kendinize mesaj gönderemezsiniz, lütfen başka bir alıcı seçin." });
            }
            _messageService.TUpdate(message);
            return Json(new { success = true });
        }


        [HttpGet]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            var users = _userManager.Users.ToList();
            List<SelectListItem> userList = (from x in users.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = $"{x.UserName} - {x.Name} {x.Surname} - {x.Email}",
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.userList = userList;
            return View();
        }
        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createMessageDto.SenderId = user.Id;
            createMessageDto.SendingTime = DateTime.Now;
            createMessageDto.IsDraft = false;
            Message message = new Message()
            {
                IsImportant = createMessageDto.IsImportant,
                IsRead = createMessageDto.IsRead,
                IsJunk = createMessageDto.IsJunk,
                IsDraft = createMessageDto.IsDraft,
                ReceiverId = createMessageDto.ReceiverId,
                SenderId = createMessageDto.SenderId,
                Content = createMessageDto.Content,
                Subject = createMessageDto.Subject,
                ReadingTime = createMessageDto.ReadingTime,
                SendingTime = createMessageDto.SendingTime,
                MessageId = createMessageDto.MessageId,
            };

            if (message.ReceiverId == message.SenderId)
            {
                return Json(new { success = false, message = "Kendinize mesaj gönderemezsiniz, lütfen başka bir alıcı seçin." });
            }
            _messageService.TInsert(message);
            return Json(new { success = true });

        }

        [HttpPost]
        [Route("MakeMessagesImportant")]
        public IActionResult MakeMessagesImportant([FromBody] List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                return Json(new { success = false, message = "Geçersiz mesaj ID'leri" });
            }

            foreach (var messageId in messageIds)
            {
                var message = _messageService.TGetById(messageId);
                if (message != null)
                {
                    message.IsImportant = true;
                    message.IsJunk = false;
                    message.IsDraft = false;
                    _messageService.TUpdate(message);
                }
            }

            return Json(new { success = true, message = "Mesajlar önemli olarak işaretlendi." });
        }

        [HttpPost]
        [Route("MakeMessagesUnImportant")]
        public IActionResult MakeMessagesUnImportant([FromBody] List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                return Json(new { success = false, message = "Geçersiz mesaj ID'leri" });
            }

            foreach (var messageId in messageIds)
            {
                var message = _messageService.TGetById(messageId);
                if (message != null)
                {
                    message.IsImportant = false;
                    message.IsJunk = false;
                    message.IsDraft = false;
                    _messageService.TUpdate(message);
                }
            }

            return Json(new { success = true, message = "Mesajlar önemli kutusundan çıkartıldı." });
        }

        [HttpPost]
        [Route("MakeMessagesJunk")]
        public IActionResult MakeMessagesJunk([FromBody] List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                return Json(new { success = false, message = "Geçersiz mesaj ID'leri" });
            }

            foreach (var messageId in messageIds)
            {
                var message = _messageService.TGetById(messageId);
                if (message != null)
                {
                    message.IsImportant = false;
                    message.IsDraft = false;
                    message.IsJunk = true;
                    _messageService.TUpdate(message);
                }
            }

            return Json(new { success = true, message = "Mesajlar çöp kutusuna taşındı." });
        }

        [HttpPost]
        [Route("MakeMessagesUnJunk")]
        public IActionResult MakeMessagesUnJunk([FromBody] List<int> messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                return Json(new { success = false, message = "Geçersiz mesaj ID'leri" });
            }

            foreach (var messageId in messageIds)
            {
                var message = _messageService.TGetById(messageId);
                if (message != null)
                {
                    message.IsImportant = false;
                    message.IsDraft = false;
                    message.IsJunk = false;
                    _messageService.TUpdate(message);
                }
            }

            return Json(new { success = true, message = "Mesajlar çöp kutusundan çıkartıldı." });
        }

        [Route("ReadMessage/{id:int}")]
        public async Task<IActionResult> ReadMessage(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            ViewBag.userId = user.Id;
            var message = _messageService.TGetMessageByMessageId(id);
            if (message.SenderId!=user.Id)
            {
                message.IsRead = true;
                _messageService.TUpdate(message);
            }
            var messageMap = _mapper.Map<ReadMessageDto>(message);
            return View(messageMap);
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
        public async Task<IActionResult> GetMessageListBySenderId(int id)
        {
            ViewBag.senderId=id;
            var sender = await _userManager.FindByIdAsync(id.ToString());
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetMessageListBySenderId(id, user.Id);
            ViewBag.SenderName = sender.Name + " " + sender.Surname;
            var values = _mapper.Map<List<InboxMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
        [Route("GetMessageListByReceiverId/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMessageListByReceiverId(int id)
        {
            ViewBag.receiverId = id;
            var receiver = await _userManager.FindByIdAsync(id.ToString());
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _messageService.TGetMessageListByReceiverId(id, user.Id);
            ViewBag.ReceiverName = receiver.Name + " " + receiver.Surname;
            var values = _mapper.Map<List<InboxMessageListDto>>(value);
            ViewBag.inboxMessageCount = _messageService.TGetInboxMessageList(user.Id).Count();
            ViewBag.sentMessageCount = _messageService.TGetSentMessageList(user.Id).Count();
            ViewBag.draftMessageCount = _messageService.TGetDraftMessageList(user.Id).Count();
            ViewBag.junkMessageCount = _messageService.TGetJunkMessageList(user.Id).Count();
            ViewBag.importantMessageCount = _messageService.TGetImportantMessageList(user.Id).Count();
            return View(values);
        }
    }
}
