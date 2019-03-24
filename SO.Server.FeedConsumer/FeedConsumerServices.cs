using Microsoft.Extensions.DependencyInjection;

namespace SO.Server.FeedConsumer
{
    public static class FeedConsumerServices
    {
        public static IServiceCollection AddFeedConsumerServices(this IServiceCollection services)
        {
            services.AddTransient<FeedConsumerJob>();
            return services;
        }
    }
}
