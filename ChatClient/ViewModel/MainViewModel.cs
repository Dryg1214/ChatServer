using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChatClient.Model;
using ChatClient.Command;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using ChatClient.Services;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        //private HubConnection _connection = null!;
        private IChatService chatService;

        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }
        
        public ObservableCollection<UserModel> Chats { get; set; }

        public RelayCommand SendCommand { get; set; }

        private UserModel _selectedChat;
        public UserModel SelectedChat {
            get { return _selectedChat; } 
            set
            {
                _selectedChat = value;
                OnPropertyChanged();
            }
        }

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

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            LoginCommand = new RelayCommandAsync(() => Login(), (o) => CanLogin());

            Messages = new ObservableCollection<MessageModel>();
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<UserModel>();
            
            
            
            Users.Add(new UserModel
            {
                Username = $"Hello",
                Messages = Messages
            });

            Chats.Add(new UserModel
            {
                Username = $"Hello",
                Messages = Messages
            });


            var username = " ";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(LoginWindow))
                {
                    username = (window as LoginWindow).login1.Text;
                }
            }


            SendCommand = new RelayCommand(x =>
            {
                Messages.Add(new MessageModel
                {
                    Username = "Paolo",
                    Time = DateTime.Now,
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

            //Messages.Add(new MessageModel
            //{
            //    Username = "Aboba",
            //    Message = "Last",
            //    Time = DateTime.Now,
            //    IsNativeOrigin = false,
            //});

            ////добавление в список пользователей
            //for (int i = 0; i < 5; i++)
            //{
            //    Users.Add(new UserModel
            //    {
            //        Username = $"Aboba {i}",
            //        Messages = Messages
            //    });
            //}

        }

        #region Connect Command
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new RelayCommandAsync(() => Connect()));
            }
        }
        private async Task<bool> Connect()
        {
            try
            {
                await chatService.ConnectAsync();
                IsConnected = true;
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region Login Command
        
        public ICommand LoginCommand { get; }
        
        private async Task<bool> Login()
        {
            try
            {
                Dictionary<string, string> users = new Dictionary<string, string>();
                users = await chatService.LoginAsync(_userName);
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        var messages = new ObservableCollection<MessageModel>();
                        Users.Add(new UserModel
                        {
                            Username = user.Key,
                            Messages = messages
                        });
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Username is already in use");
                    return false;
                }

            }
            catch (Exception) { return false; }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && UserName.Length >= 2 && IsConnected;
        }
        #endregion



    }
}
