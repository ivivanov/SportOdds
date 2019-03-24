using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SO.Server.FeedConsumer
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly FeedConsumerJob _feedConsumer;

        public TimedHostedService(FeedConsumerJob feedConsumer)
        {
            _feedConsumer = feedConsumer;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(600));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
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
