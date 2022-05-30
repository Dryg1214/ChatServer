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

namespace ChatClient.ViewModel
{
    public class MainViewModel : ObservableObject
    {
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
        //static async Task
        public void AddChat(string users)
        {
            Users.Add(new UserModel
            {
                Username = users,
                Messages = Messages
            });
        }
        
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<UserModel>();
            Users.Add(new UserModel
            {
                Username = $"Hello",
                Messages = Messages
            });


            Action();
            /*var username = "Aboba_Vovk";
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatroom")
                .Build();
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                AnsiConsole.MarkupLine($"[bold yellow]{user}[/]: [blue]{message}[/]");
            });

            connection.On<string, string, string>("ReceiveMessageFromGroup", (groupName, user, message) =>
            {
                AnsiConsole.MarkupLine($"[bold red]{groupName}[bold yellow][/] {user}[/]: [blue]{message}[/]");
            });

            connection.On<string, string>("ReceiveDirectMessage", (user, message) =>
            {
                AnsiConsole.MarkupLine($"[bold red]{user}[/]: [blue]{message}[/]");
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Enter", username);*/
            //прочитать из файла сообщения и пользователей с сервера


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

            //Messages.Add(new MessageModel
            //{
            //    Username = "Aboba",
            //    Message = "hi, bro",
            //    Time = DateTime.Now,
            //    IsNativeOrigin = false,
            //    FirstMessage = true
            //});

            //for (int i = 0; i < 4; i++)
            //{
            //    Messages.Add(new MessageModel
            //    {
            //        Username = "SaintPaolo",
            //        Message = "hi, bro",
            //        Time = DateTime.Now,
            //        IsNativeOrigin = true
            //    });
            //}

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

        public async Task Action()
        {
            var username = " ";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(LoginWindow))
                {
                    username = (window as LoginWindow).login1.Text;
                }
            }
            
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatroom")
                .Build();
            Users.Add(new UserModel
            {
                Username = username,
                Messages = Messages
            });
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Messages.Add(new MessageModel
                {
                    Username = user,
                    Message = message,
                    Time = DateTime.Now,
                    IsNativeOrigin = true
                });
            });

            connection.On<string, string, string>("ReceiveMessageFromGroup", (groupName, user, message) =>
            {
                
                //AnsiConsole.MarkupLine($"[bold red]{groupName}[bold yellow][/] {user}[/]: [blue]{message}[/]");
            });

            connection.On<string, string>("ReceiveDirectMessage", (user, message) =>
            {
                Messages.Add(new MessageModel
                {
                    Username = user,
                    Message = message,
                    Time = DateTime.Now,
                    IsNativeOrigin = true
                });
                //AnsiConsole.MarkupLine($"[bold red]{user}[/]: [blue]{message}[/]");
            });

            await connection.StartAsync();

            await connection.InvokeAsync("Enter", username);

            while (true)
            {
                
                var message = Message;
                /*
                if (message == "exit") break;

                if (message.StartsWith("+"))
                {
                    await connection.InvokeAsync("JoinGroup", username, message.Split('+', ' ')[1]);
                }
                else if (message.StartsWith("-"))
                {
                    await connection.InvokeAsync("LeaveGroup", username, message.Split('-', ' ')[1]);
                }

                else if (message.StartsWith("#"))
                {
                    var groupName = message.Split('#', ' ')[1];
                    var messageToSend = message.Replace("#" + groupName, "");
                    await connection.InvokeAsync("SendMessageToGroup", groupName, username, message);
                }

                else if (message.StartsWith("@"))
                {
                    var receiver = message.Split('@', ' ')[1];
                    var messageToSend = message.Replace("@" + receiver, "");
                    await connection.InvokeAsync("SendMessageToUser", username, message, receiver);
                }*/

                if (message != null)
                    await connection.InvokeAsync("SendMessage", username, message);
                
            }
            await connection.StopAsync();
        }

    }
}
