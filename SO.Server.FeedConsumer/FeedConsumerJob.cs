using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SO.Data;
using SO.Data.Entities;
using SO.Data.Repositories;
using SO.Server.FeedConsumer.Comparers;
using SO.Server.FeedConsumer.Feeds;
using SO.Server.FeedConsumer.Models;
using SO.Server.FeedConsumer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SO.Server.FeedConsumer
{
    public class FeedConsumerJob
    {
        public delegate void MatchesUpdatedHandler(IEnumerable<MatchModel> updatedMatches);
        public delegate void MatchesRemovedHandler(IEnumerable<int> removedMatches);
        public delegate void MatchesAddedHandler(IEnumerable<MatchModel> addedMatches);
        public event MatchesUpdatedHandler matchesUpdatedEvent;
        public event MatchesAddedHandler matchesAddedEvent;
        public event MatchesRemovedHandler matchesRemovedEvent;
        private const int SoccerId = 1165; // used just for POC
        private readonly IMapper _mapper;
        private readonly IServiceProvider _services;
        private readonly IFeed _feed;

        public FeedConsumerJob(IMapper mapper, IServiceProvider services, IFeed feed)
        {
            _mapper = mapper;
            _services = services;
            _feed = feed;
        }

        public void Fetch()
        {
            using (var uow = (IUnitOfWork)_services.GetService(typeof(IUnitOfWork)))
            {
                var sportRepository = uow.GetRepository<Sport>();

                //can be stored in memory and update it after each feed pull 
                var currentSport = sportRepository
                         .FirstOrDefault(x => x.Id == SoccerId,
                                 include: x =>
                                     x.Include(sp => sp.Events)
                                     .ThenInclude(ev => ev.Matches)
                                     .ThenInclude(ma => ma.Bets)
                                     .ThenInclude(bet => bet.Odds));

                //var updatedSport = _feed.GetSports(SoccerId);
                var updatedSport = XmlUtils.Deserialize<XmlSportsModel>(XmlUtils.GetXmlString("soccer.xml"));
                var currentSportModel = _mapper.Map<SportModel>(currentSport);

                if (currentSport == null)// Initial DB seed
                {
                    var newSport = _mapper.Map<Sport>(updatedSport.Sport);
                    sportRepository.Add(newSport);
                }
                else
                {
                    var syncRequirement = DiffChecker.GetSyncRequirement(currentSportModel.Events, updatedSport.Sport.Events);
                    if (syncRequirement.IsSyncRequired)
                    {
                        //this is the proper way for db Sync
                        //eventRepository.Sync(updatedEventEntities);

                        // this is a workaround on Sync method. For more info check README.md
                        SyncEventInDb(currentSport, updatedSport.Sport, syncRequirement, uow);
                    }
                }

                uow.BulkSaveChanges();
            }
        }

        private void SyncEventInDb(Sport currentSport, SportModel updatedSportModel, SyncRequirement syncRequirement, IUnitOfWork uow)
        {
            var toAddModel = updatedSportModel.Events.Where(x => syncRequirement.Add.Contains(x.Id));
            var toAdd = _mapper.Map<IEnumerable<Event>>(toAddModel);
            toAdd = toAdd.Select(x =>
            {
                x.SportId = currentSport.Id;
                return x;
            });

            var toUpdateModels = updatedSportModel.Events.Where(x => syncRequirement.Update.Contains(x.Id));
            var toUpdate = _mapper.Map<IEnumerable<Event>>(toUpdateModels);
            toUpdate = toUpdate.Select(x =>
            {
                x.SportId = currentSport.Id;
                return x;
            });

            var toDelete = _mapper.Map<IEnumerable<Event>>(currentSport.Events.Where(x => syncRequirement.Delete.Contains(x.Id)));

            var eventRepository = uow.GetRepository<Event>();

            eventRepository.Delete(toDelete);
            eventRepository.Add(toAdd);
            eventRepository.Update(toUpdate);

            if (toUpdateModels.Any())
                matchesUpdatedEvent?.Invoke(toUpdateModels.SelectMany(x => x.Matches));
            if (toAddModel.Any())
                matchesAddedEvent?.Invoke(toAddModel.SelectMany(x => x.Matches));
            if (syncRequirement.Delete.Any())
                matchesRemovedEvent?.Invoke(syncRequirement.Delete);
        }
    }
}
