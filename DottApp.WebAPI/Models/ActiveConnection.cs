using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DottApp.WebAPI.Models
{
    public class ActiveConnection
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string SessionId { get; set; }
        public bool IsAuth { get; set; }
        public string LoginName { get; set; }
        public string AesKey { get; set; }
        public DateTime ConnectionDate { get; set; }
    }
}
