using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Server.Hubs;

namespace Server.Extensions
{
    public static class EndpointExtensions
    {
        public static void ConfigureHubMaps(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<MessageHub>("/message");
        }
    }
}
