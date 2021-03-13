using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DottApp.WebAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public ChatType Type { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }

        public enum ChatType
        {
            Regular, Group
        }
    }
}
