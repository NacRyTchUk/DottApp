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
            var session = _context.GetSession(sid);
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));
            var msg = aesw.Deserialize<MsgSendRequest>(body);

            var senderId = _context.Users.Where(a => a.LoginName == session.LoginName).ToArray()[0].Id;
            _context.HistoryMessages.Add(new HistoryMessage()
            {
                SenderId = senderId,
                SendTime = DateTime.Now,
                Text = msg.Text,
                ReceiverId = msg.ReceiverId,
            });
            

            _context.SaveChanges();

            return Ok();
        }
    }
}
