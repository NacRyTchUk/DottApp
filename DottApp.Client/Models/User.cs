using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Client.Models
{
    class User
    {
        public string NickName { get; set; }

        public DateTime DateRegistration { get; set; }

        public DateTime DateOfLastActivity { get; set; }
        
        public string Login { get; set; }
        public string Photo { get; set; }

        public User(string NickName, string Login, string Photo)
        {
            this.NickName = NickName;
            this.DateOfLastActivity = DateTime.Now;
            this.DateRegistration = DateTime.Now;
            this.Login = Login;
            this.Photo = Photo;
        }
    }
}
