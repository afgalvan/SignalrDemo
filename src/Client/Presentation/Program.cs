using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Settings;

namespace Presentation
{
    internal static class Program
    {
        private static async Task Main() =>
            await CreateHostBuilder().RunConsoleAsync();

        private static IHostBuilder CreateHostBuilder() =>
            new HostBuilder()
                .UseConsoleLifetime()
                .ConfigureServices(InitDefaultStartup);

        private static void InitDefaultStartup(IServiceCollection services)
        {
            new Startup(Configuration.Startup).ConfigureServices(services);
        }

        private static async Task StarApplication(IServiceCollection services)
        {
            await using ServiceProvider provider = services.BuildServiceProvider();
            var hubConnection = provider.GetRequiredService<HubConnection>();
            await new ConsoleApp(hubConnection).StartAsync(new CancellationToken());
        }
    }
}
