using Microsoft.Extensions.DependencyInjection;
using SO.Server.FeedConsumer.Feeds;

namespace SO.Server.FeedConsumer
{
    public static class FeedConsumerServices
    {
        public static IServiceCollection AddFeedConsumerServices(this IServiceCollection services)
        {
            services.AddScoped<FeedConsumerJob>();
            services.AddTransient<IFeed, UltraplayFeedClient>();
            return services;
        }
    }
}
