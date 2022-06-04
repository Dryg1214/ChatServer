using ChatClient.Command;
using ChatClient.Model;
using ChatClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }

        public RelayCommand SendCommand { get; set; }

        private UserModel _selectedChat;
        public UserModel SelectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                OnPropertyChanged();
            }
        }

        private string? _message;

        public string? Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IChatService chatService)
        {
            chatService.UserJoined.Subscribe(login =>
            {
                Application.Current.Dispatcher.VerifyAccess();
                // Users.Add(login);
            });
            chatService.MessageReceived.Subscribe(message =>
            {
                Application.Current.Dispatcher.VerifyAccess();
                // TODO
            });

            //LoginCommand = new RelayCommandAsync(() => Login(), (o) => CanLogin());

            Messages = new ObservableCollection<MessageModel>();
            Users = new ObservableCollection<UserModel>();
            
        }

      

       



    }
}
