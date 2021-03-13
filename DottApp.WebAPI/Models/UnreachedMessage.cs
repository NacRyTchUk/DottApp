using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.WebAPI.Models.Base;

namespace DottApp.WebAPI.Models
{
    public class UnreachedMessage
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int ReceiverId { get; set; }
    }
}
