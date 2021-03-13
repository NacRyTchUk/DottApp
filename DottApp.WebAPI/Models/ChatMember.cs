using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DottApp.WebAPI.Models
{
    public class ChatMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public bool IsEnter { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
