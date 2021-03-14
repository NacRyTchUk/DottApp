using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Client.Models
{
    class Dialogs
    {
        internal class Chat
        {
            public string NickName { get; set; }

            public DateTime DateLastMessage { get; set; }

            public int NumofMessagesUnRead { get; set; }

            public IList<Message> Messages { get; set; }

            public ChatType ChatType { get; set; }

            public int NumOfMembers { get; set; }

            public string Photo { get; set; }

            public Chat(User User, ChatType ChatType, IList<Message> Messages)
            {
                DateLastMessage = DateTime.Now;
                NickName = User.NickName;
                this.ChatType = ChatType;
                NumOfMembers = 0;
                this.Messages = Messages;
                this.NumofMessagesUnRead = 1;
                Photo = User.Photo;
            }
        }

        internal class Message
        {
            public string Text { get; set; }

            public DateTime DateMessageSent { get; set; }

            public bool IsRead { get; set; }

            public bool IsEdited { get; set; }

            public Attachment Attachments { get; set; }

            public User Sender { get; set; }

        }
        public abstract class Attachment
        {
            //bruh
        }
        public enum ChatType
        {
            Regular, Group
        }
    }
}