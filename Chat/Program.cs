using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Spectre.Console;

namespace Chat
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var username = AnsiConsole.Ask<string>("what's your name?");
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

            await connection.InvokeAsync("Enter", username);

            while (true)
            {
                var message = AnsiConsole.Ask<string>($"{username}: ");
                
                if (message == "exit") break;
                
                if(message.StartsWith("+"))
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
                }

                else
                {
                    await connection.InvokeAsync("SendMessage", username, message);
                }
            }
            await connection.StopAsync();
        }
    }
}
