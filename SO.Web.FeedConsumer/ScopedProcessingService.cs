using Microsoft.AspNetCore.SignalR;
using SO.Data;
using SO.Server.FeedConsumer;
using SO.Server.FeedConsumer.Models;
using SO.Web.FeedConsumer.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace SO.Web.FeedConsumer
{
    internal interface IScopedProcessingService
    {
        void DoWork(IUnitOfWork uow);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly FeedConsumerJob _feedConsumerJob;
        private readonly IHubContext<SportUpdatesHub> _sportUpdatesHub;

        public ScopedProcessingService(FeedConsumerJob feedConsumerJob, IHubContext<SportUpdatesHub> sportUpdatesHub)
        {
            _feedConsumerJob = feedConsumerJob;
            _sportUpdatesHub = sportUpdatesHub;

            _feedConsumerJob.eventsAddedEvent += EventsAddedEventHandler;
            _feedConsumerJob.eventsUpdatedEvent += EventsUpdatedEventHandler;
            _feedConsumerJob.eventsRemovedEvent += EventsRemovedEventHandler;

        }

        private void EventsRemovedEventHandler(IEnumerable<int> removedEvents)
        {
            _sportUpdatesHub.Clients.All.SendAsync("EventsRemove", removedEvents);
        }

        private void EventsUpdatedEventHandler(IEnumerable<EventModel> updatedEvents)
        {
            _sportUpdatesHub.Clients.All.SendAsync("OddsUpdate", updatedEvents.SelectMany(x => x.Matches).SelectMany(x => x.Bets).SelectMany(x => x.Odds));
        }

        private void EventsAddedEventHandler(IEnumerable<EventModel> addedEvents)
        {
            _sportUpdatesHub.Clients.All.SendAsync("EventsAdd", addedEvents);
        }

        private void _feedConsumer_matchChangedEvent(IEnumerable<int> changedMatches)
        {
        }

        public void DoWork(IUnitOfWork uow)
        {
            _sportUpdatesHub.Clients.All.SendAsync("ServerStatus", "Server is checking for changes");
            _feedConsumerJob.Fetch();
        }
    }
}
