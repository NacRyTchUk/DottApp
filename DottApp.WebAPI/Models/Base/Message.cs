using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DottApp.WebAPI.Models.Base
{
    public class Message
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ChatId { get; set; }
        public DateTime SendTime { get; set; }
        public int AttachmentId { get; set; }
        public bool IsEdited { get; set; }
        public bool IsRead { get; set; }
    }
}
