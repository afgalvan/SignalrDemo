using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
        }

        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<ConsoleApp>();
        }


        public static void AddHubConnections(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(CreateMessageHub(configuration));
        }

        private static HubConnection CreateMessageHub(IConfiguration configuration)
        {
            return new HubConnectionBuilder()
                .WithUrl($"{configuration["Server:Host"]}/message")
                .AddMessagePackProtocol().Build();
        }
    }
}
