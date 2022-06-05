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
        public ObservableCollection<MessageModel> Messages { get; set; } = new();

        public ObservableCollection<UserModel> Users { get; set; } = new ();

        public RelayCommand? SendCommand { get; set; }

        private UserModel? _selectedUser;
        public UserModel? SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        private string? _message;

        public string? _loginUser;
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
                //Application.Current.Dispatcher.VerifyAccess();
                Users.Add(new UserModel
                {
                    Username = login,
                    Messages = Messages
                });
            });
            chatService.MessageReceived.Subscribe(message =>
            {
                //Application.Current.Dispatcher.VerifyAccess();
                //// TODO
            });

            SendCommand = new RelayCommand(x =>
            {
                Messages.Add(new MessageModel(_loginUser!, Message!));
                Message = "";
            });
        }







    }
}
