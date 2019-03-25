using Microsoft.AspNetCore.SignalR;
using SO.Server.FeedConsumer;
using SO.Server.FeedConsumer.Models;
using SO.Web.Api.DataFilters;
using SO.Web.Api.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace SO.Web.Api.BackgroundWorkers
{
    internal interface IScopedProcessingService
    {
        void DoWork();
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly FeedConsumerJob _feedConsumerJob;
        private readonly IHubContext<SportUpdatesHub> _sportUpdatesHub;

        public ScopedProcessingService(FeedConsumerJob feedConsumerJob, IHubContext<SportUpdatesHub> sportUpdatesHub)
        {
            _feedConsumerJob = feedConsumerJob;
            _sportUpdatesHub = sportUpdatesHub;

            _feedConsumerJob.matchesAddedEvent += MatchesAddedEventHandler;
            _feedConsumerJob.matchesUpdatedEvent += MatchesUpdatedEventHandler;
            _feedConsumerJob.matchesRemovedEvent += MatchesRemovedEventHandler;
        }

        private void MatchesRemovedEventHandler(IEnumerable<int> removedMatches)
        {
            _sportUpdatesHub.Clients.All.SendAsync("matchesRemoved", removedMatches);
        }

        private void MatchesUpdatedEventHandler(IEnumerable<MatchModel> updatedMatches)
        {
            _sportUpdatesHub.Clients.All.SendAsync("oddsUpdated", 
                updatedMatches
                .AsQueryable()
                .Where(MatchesForNext24hoursPredicate.ModelPredicate)
                .SelectMany(x => x.Bets)
                .SelectMany(x => x.Odds));
        }

        private void MatchesAddedEventHandler(IEnumerable<MatchModel> addedMatches)
        {
            _sportUpdatesHub.Clients.All.SendAsync("matchesAdded", addedMatches);
        }

        public void DoWork()
        {
            _feedConsumerJob.Fetch();
        }
    }
}
