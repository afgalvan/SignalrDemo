using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedLib;

namespace Presentation
{
    internal static class Program
    {
        private static HubConnection _hubConnection;

        private static async Task Main()
        {
            await ConnectWithHub();
            Console.WriteLine("Connected to Hub");
            SetupHubHandlers();
            await ReadUserInput();
        }

        private static async Task ReadUserInput()
        {
            string message;
            while ((message = Console.ReadLine()) != "exit")
            {
                await _hubConnection.SendAsync("Send", new MessageDto
                {
                    Username = "Albert", Content = message
                });
            }

            await _hubConnection.DisposeAsync();
        }

        private static void SetupHubHandlers()
        {
            _hubConnection.On<MessageDto>("Send",
                response =>
                {
                    Console.WriteLine(
                        $"{response.Username}: {response.Content}");
                });
        }

        private static async Task ConnectWithHub()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://5e03-186-169-89-224.ngrok.io/message")
                .AddMessagePackProtocol()
                .ConfigureLogging(factory =>
                {
                    factory.AddFilter("Console",
                        level => level >= LogLevel.Trace);
                }).Build();

            await _hubConnection.StartAsync();
        }
    }
}
