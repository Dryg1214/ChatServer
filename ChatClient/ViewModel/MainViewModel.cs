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
        private IChatService _chatService;
        public ObservableCollection<MessageModel> Messages { get; set; } = new();

        public ObservableCollection<UserModel> Users { get; set; } = new ();

        public RelayCommand? SendCommand { get; set; }

        private UserModel? _selectedUser;
        public UserModel? SelectedUser
        {
            get 
            {
                //if (_selectedUser != null)
                //{

                //    //foreach (Window window in Application.Current.Windows)
                //    //{
                //    //    if (window.GetType() == typeof(MainWindow))
                //    //    {
                //    //        (window as MainWindow).ListMessages.Items.Clear();
                //    //        (window as MainWindow).ListMessages.Items.Add(_selectedUser.Messages);
                //    //    }
                //    //}
                //    foreach (var user in Users)
                //    {
                //        if (user.Username == _selectedUser.Username)
                //            Messages = user.Messages!;
                //    }
                    
                //}
                return _selectedUser; 
            }
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

        #region Send Text Message Command
        private ICommand _sendTextMessageCommand;
        public ICommand SendTextMessageCommand
        {
            get
            {
                return _sendTextMessageCommand ?? (_sendTextMessageCommand =
                    new RelayCommandAsync(SendTextMessage));
            }
        }
        private async Task<bool> SendTextMessage()
        {
            try
            {
                if (_selectedUser == null)
                    throw new InvalidOperationException();
                var receiver = _selectedUser.Username;
                await _chatService.SendMessage(receiver, Message);
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                var newMessage = new MessageModel(_loginUser!, Message!);
                _selectedUser.Messages.Add(newMessage);
                Message = string.Empty;
            }
        }
        #endregion

        public MainViewModel(IChatService chatService)
        {
            _chatService = chatService;
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
                //foreach(var user in Users)
                //{
                //    if (user.Username == message.Sender)
                //        user.Messages.Add(message);
                //}
                Messages.Add(message);
            });

            //SendCommand = new RelayCommand(x =>
            //{
            //    Messages.Add(new MessageModel(_loginUser!, Message!));
            //});
        }







    }
}
