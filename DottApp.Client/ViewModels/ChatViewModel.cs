using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DottApp.Client.Infrastructure.Commands;
using DottApp.Client.Models;
using DottApp.Client.ViewModels.Base;
using static DottApp.Client.Models.Dialogs;

namespace DottApp.Client.ViewModels
{
    internal class ChatViewModel : Base.ViewModel
    {
        public ObservableCollection<Chat> Chats { get; }

        public object[] CompositeCollection { get; }

        #region SelectedCompositeValue : object - Выбранный непонятный элемент

        /// <summary>Выбранный непонятный элемент</summary>
        private object _SelectedCompositeValue;

        /// <summary>Выбранный непонятный элемент</summary>
        public object SelectedCompositeValue { get => _SelectedCompositeValue; set => Set(ref _SelectedCompositeValue, value); }

        #endregion

        #region SelectedChat : Chat - Выбранная группа

        /// <summary>Выбранная группа</summary>
        private Chat _SelectedChat;

        /// <summary>Выбранная группа</summary>
        public Chat SelectedChat
        {
            get => _SelectedChat;
            set => Set(ref _SelectedChat, value);
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var new_chat = new Chat(
                new User("Bruh", "bruh", null),
                ChatType.Regular,
                new ObservableCollection<Message>());
            Chats.Add(new_chat);
        }

        #region DeleteGroupCommand

        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecuted(object p) => p is Chat chat && Chats.Contains(chat);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Chat chat)) return;
            var chat_index = Chats.IndexOf(chat);
            Chats.Remove(chat);
            if (chat_index < Chats.Count)
                SelectedChat = Chats[chat_index];
        }

        #endregion

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        public ChatViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecuted);

            #endregion
            User[] users = new User[5];
            users[0] = new User("Николай Гончаров", "StarostaLoh", "pack://application:,,,/Data/face1.jpg");
            users[1] = new User("Олеся Литвинова", "da", "pack://application:,,,/Data/face2.jpg");
            users[2] = new User("Лилеся Отвинова", "net", "pack://application:,,,/Data/face3.png");
            users[3] = new User("Федор Лозбень", "shakal", "pack://application:,,,/Data/shakal.jpg");
            users[4] = new User("Макаронный монстр", "sebya_pozovi", "pack://application:,,,/Data/monstr.jpeg");
            Message[] messages1 = new Message[3];
            messages1[0] = new Message
            {
                Text = $"@sebya_pozovi, когда поздравляем? Где время?",
                IsEdited = false,
                IsRead = false,
                Attachments = null,
                Sender = users[0],
                DateMessageSent = DateTime.Now
            };
            
            messages1[1] = new Message
            {
                Text = "да",
                IsEdited = false,
                IsRead = false,
                Attachments = null,
                Sender = users[0],
                DateMessageSent = DateTime.Now
            };
            messages1[2] = new Message
            {
                Text = "нет",
                IsEdited = false,
                IsRead = false,
                Attachments = null,
                Sender = users[0],
                DateMessageSent = DateTime.Now
            };
            Message[] messages2 = new Message[2];
            messages2[0] = new Message
            {
                Text = "Спасибо за лекцию",
                IsEdited = false,
                IsRead = false,
                Attachments = null,
                Sender = users[0],
                DateMessageSent = DateTime.Now
            };
            messages2[1] = new Message
            {
                Text = "Есть 3500 на 2 дня?",
                IsEdited = false,
                IsRead = false,
                Attachments = null,
                Sender = users[0],
                DateMessageSent = DateTime.Now
            };
            Chat[] chats = new Chat[5];
            for(int i = 0; i < 3; ++i)
            chats[i] = new Chat(users[i], ChatType.Regular, new ObservableCollection<Message>(messages1));
            for (int i = 3; i < 5; ++i)
                chats[i] = new Chat(users[i], ChatType.Regular, new ObservableCollection<Message>(messages2));
            Chats = new ObservableCollection<Chat>(chats);
        }

    }
    //#region ChatWidth
    //private string _Width = "380";
    //public string Width
    //{
    //    get => _Width;
    //    set => Set(ref _Width, value);
    //}
    //#endregion
}
