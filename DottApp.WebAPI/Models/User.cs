using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DottApp.WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastOnline { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
