using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChatClient.Model;
using ChatClient.Command;

namespace ChatClient.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }

        public RelayCommand SendCommand { get; set; }
        public UserModel SelectedChat { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Users = new ObservableCollection<UserModel>();

            SendCommand = new RelayCommand(x =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";
            });

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
