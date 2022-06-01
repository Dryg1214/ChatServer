using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public interface IChatService
    {
        Task ConnectAsync();
        Task<Dictionary<string, string>> LoginAsync(string name);
        Task JoinGroup(string user, string groupName);
        Task LeaveGroup(string user, string groupName);
        Task SendMessageToGroup(string groupName, string user, string message);
        Task SendMessageToUser(string user, string message, string receiver);


    }
}
