using Microsoft.Extensions.Hosting;
using SO.Server.FeedConsumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SO.Web.Client.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly FeedConsumerJob _feedConsumer;

        public TimedHostedService(FeedConsumerJob feedConsumer)
        {
            _feedConsumer = feedConsumer;
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is starting.");


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            _feedConsumer.Fetch();
        }
    }
}
