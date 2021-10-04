using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using SharedLib;

namespace Presentation
{
    public class ConsoleApp : IHostedService
    {
        private readonly HubConnection _hubConnection;
        private          string        _username;

        public ConsoleApp(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ConnectWithChatHub();
            Console.WriteLine("Connected to Hub");
            SetupHubHandlers();
            AskUsername();
            await ReadUserInput();
        }

        private void AskUsername()
        {
            Console.Write("Username: ");
            _username = Console.ReadLine();
        }

        private async Task ReadUserInput()
        {
            string message;
            while ((message = Console.ReadLine()) != "exit")
            {
                await _hubConnection.SendAsync("Send", new MessageDto
                {
                    Username = _username, Content = message
                });
            }

            await _hubConnection.DisposeAsync();
            Process.GetCurrentProcess().Kill();
        }

        private void SetupHubHandlers()
        {
            _hubConnection.On<MessageDto>("Send",
                response =>
                {
                    Console.WriteLine(
                        $"{response.Username}: {response.Content}");
                });
        }

        private async Task ConnectWithChatHub()
        {
            await _hubConnection.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
