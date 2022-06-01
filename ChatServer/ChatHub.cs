using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatClient.Model;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer
{
    public class ChatHub : Hub
    {
        private Dictionary<string, string> Connections = new();

        static List<string> Users = new List<string>();
        public Task SendMessage(string user, string message)
        {
            return Clients.Others.SendAsync("ReceiveMessage", user, message);
        }
        public Task Login(string user)
        {
            Connections[user] = Context.ConnectionId;
            return Clients.Others.SendAsync("ReceiveMessage", user, $"{user} is connected");
        }
        public List<string> LoginAsync(string name)
        {
            if (!Connections.ContainsKey(name))
            {
                Console.WriteLine($"++ {name} logged in");
                List<string> users = new List<string>(Connections.Values);
                
                var added = Connections.TryAdd(name, Context.ConnectionId);
                if (!added) return null;
                Clients.Caller. = name;
                Clients.CallerState.UserName = name;
                return users;
            }
            return null;
        }


        public async Task JoinGroup(string user, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessageFromGroup", groupName, user, "has joined the group");
        }

        public async Task LeaveGroup(string user, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessageFromGroup", groupName, user, "has left ed the group");
        }

        public Task SendMessageToGroup(string groupName, string user, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveMessageFromGroup", groupName, user, message);
        }

        public Task SendMessageToUser(string user, string message, string receiver)
        {
            return Clients.Client(Connections[receiver]).SendAsync("ReceiveDirectMessage", user, message);
        }

    }
}