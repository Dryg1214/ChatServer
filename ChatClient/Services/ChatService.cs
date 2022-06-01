using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class ChatService : IChatService
    {
        private HubConnection _connection;
        private string _url = "http://localhost:5000/chatex";
        public async Task ConnectAsync()
        {
            _connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:5000/chatroom")
               .Build();

            /*
              _connection.On<IEnumerable<string>>("UpdateUsersAsync", users =>
            {
                UserList = new ObservableCollection<string>(users);
            });

            _connection.On<string, string>("SendMessageAsync", (user, message) =>
            {
                var item = $"{user} says {message}";
                MessageList.Add(item);
            });
             */

            _connection.On<string, string>("ReceiveMessage", (user, message) => { });

            _connection.On<string, string, string>("ReceiveMessageFromGroup", (groupName, user, message) => { });

            _connection.On<string, string>("ReceiveDirectMessage", (user, message) => { });

            await _connection.StartAsync();
        }
        public async Task Login(string user)
        {
            await _connection.InvokeAsync("Login", user);
        }
        public async Task JoinGroup(string user, string groupName)
        {
            await _connection.InvokeAsync("JoinGroup", user, groupName);
        }
        public async Task LeaveGroup(string user, string groupName)
        {
            await _connection.InvokeAsync("LeaveGroup", user, groupName);
        }
        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await _connection.InvokeAsync("SendMessageToGroup", groupName, user, message);
        }
        public async Task SendMessageToUser(string user, string message, string receiver)
        {
            await _connection.InvokeAsync("SendMessageToUser", user, message, receiver);
        }
    }
}
