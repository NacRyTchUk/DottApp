using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.Api.Rest.Request_Response;
using DottApp.RsaAesWrapper;
using DottApp.WebAPI.Models;

namespace DottApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MessagesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("Send")]
        public IActionResult Send(string sid, string body)
        {
            Response.ContentType = "text/plain";

            var session = _context.ActiveConnections.Where(cont => cont.SessionId == sid).ToArray()[0]; ;
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));
            var msg = aesw.Deserialize<MsgSendRequest>(body);

            var senderId = _context.Users.Where(a => a.LoginName == session.LoginName).ToArray()[0].Id;


            Chat chat;

            if (msg.ReceiverId >= 0)
            {
                string chatShortInfo = $"{Math.Max(senderId, msg.ReceiverId)}+{Math.Min(senderId, msg.ReceiverId)}";
                if (_context.Chats.Where(a => a.ShortInfo == chatShortInfo).ToArray().Length > 0)
                    chat = _context.Chats.Where(a => a.ShortInfo == chatShortInfo).ToArray()[0];
                else
                {
                    DateTime time = DateTime.Now;
                    _context.Chats.Add(new Chat()
                    {
                        CreateDate = time = DateTime.Now,
                        CreatorId = senderId,
                        ShortInfo = chatShortInfo,
                        Type = Chat.ChatType.Regular
                    });
                    chat = _context.Chats.Where(a => a.CreateDate == time).ToArray()[0];
                }
            }
            else
            {
                if (_context.Chats.Where(a => a.ShortInfo == msg.ReceiverId.ToString()).ToArray().Length > 0)
                    chat = _context.Chats.Where(a => a.ShortInfo == msg.ReceiverId.ToString()).ToArray()[0];
                else
                {
                    DateTime time = DateTime.Now;
                    _context.Chats.Add(new Chat()
                    {
                        CreateDate = time = DateTime.Now,
                        CreatorId = senderId,
                        ShortInfo = msg.ReceiverId.ToString(),
                        Type = Chat.ChatType.Group
                    });
                    chat = _context.Chats.Where(a => a.CreateDate == time).ToArray()[0];
                }
            }


            DateTime stime = DateTime.Now;
            _context.HistoryMessages.Add(new HistoryMessage()
            {
                SenderId = senderId,
                SendTime = stime = DateTime.Now,
                Text = msg.Text,
                ReceiverId = msg.ReceiverId,
                ChatId = chat.Id
            });
            var hMsg = _context.HistoryMessages.Where(a => a.SendTime == stime).ToArray()[0];

            _context.UnreachedMessages.AddAsync(new UnreachedMessage()
            {
                MessageId = hMsg.Id,
                ReceiverId = msg.ReceiverId //TODO: Переделать для групп!
            });

            _context.SaveChanges();

            return Ok();
        }
    }
}
