using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SO.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SO.Web.FeedConsumer
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public TimedHostedService(IServiceProvider services)
        {
            _services = services;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
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
            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider
                    .GetRequiredService<IScopedProcessingService>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                scopedProcessingService.DoWork(uow);
            }
        }
    }
}
