using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SO.Server.Data;

namespace SO.Server.FeedConsumer
{
    public class Program
    {
        static void Main()
        {
            new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddFeedConsumerServices()
                        .AddDataServices()
                        .AddAutoMapper()
                        .AddHostedService<TimedHostedService>();
            })
            .Start();
        }
    }
}
