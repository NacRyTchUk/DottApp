using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.WebAPI.Models.Base;

namespace DottApp.WebAPI.Models
{
    public class HistoryMessage : Message
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
