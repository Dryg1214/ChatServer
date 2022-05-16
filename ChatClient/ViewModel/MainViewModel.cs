using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChatClient.Model;

namespace ChatClient.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Users = new ObservableCollection<UserModel>();

            Messages.Add(new MessageModel
            {
                Username = "Aboba",
                Message = "hi, bro",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "SaintPaolo",
                    Message = "hi, bro",
                    Time = DateTime.Now,
                    IsNativeOrigin = true
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Aboba",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = false,
            });

            for (int i = 0; i < 5; i++)
            {
                Users.Add(new UserModel
                {
                    Username = $"Aboba {i}",
                    Messages = Messages
                });
            }

        }
    }
}
