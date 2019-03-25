using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SO.Data;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Comparers;
using SO.Server.FeedConsumer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer
{
    public class FeedConsumerJob
    {
        public delegate void EventsUpdatedHandler(IEnumerable<EventModel> updatedEvents);
        public event EventsUpdatedHandler eventsUpdatedEvent;

        public delegate void EventsAddedHandler(IEnumerable<EventModel> addedEvents);
        public event EventsAddedHandler eventsAddedEvent;

        public delegate void EventsRemovedHandler(IEnumerable<int> removedEvents);
        public event EventsRemovedHandler eventsRemovedEvent;

        private readonly IMapper _mapper;
        private readonly IServiceProvider _services;

        public FeedConsumerJob(IMapper mapper, IServiceProvider services)
        {
            _mapper = mapper;
            _services = services;
        }

        public void Fetch()
        {
            var uow = (IUnitOfWork)_services.GetService(typeof(IUnitOfWork));
            var sportRepository = uow.GetRepository<Sport>();

            var currentSport = sportRepository
                     .FirstOrDefault(x => x.Id == 1165, //Just for POC 
                             include: x =>
                                 x.Include(sp => sp.Events)
                                 .ThenInclude(ev => ev.Matches)
                                 .ThenInclude(ma => ma.Bets)
                                 .ThenInclude(bet => bet.Odds));

            var updatedSportXml = Get("soccer.xml");
            var currentSportModel = _mapper.Map<SportModel>(currentSport);

            if (currentSport == null)// Initial DB seed
            {
                sportRepository.Add(_mapper.Map<Sport>(updatedSportXml.Sport));
            }
            else
            {
                var syncRequirement = DiffChecker.GetSyncRequirement(currentSportModel.Events, updatedSportXml.Sport.Events);
                if (syncRequirement.Add.Count() > 0 || syncRequirement.Delete.Count() > 0 || syncRequirement.Update.Count() > 0)
                {
                    SyncEventInDb(currentSport, updatedSportXml.Sport, syncRequirement, uow);
                }
            }

            uow.SaveChanges();
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

            var sportRepository = uow.GetRepository<Sport>();
            var eventRepository = uow.GetRepository<Event>();

            eventRepository.Delete(toDelete);
            eventRepository.Add(toAdd);
            eventRepository.Update(toUpdate);

            eventsUpdatedEvent?.Invoke(toUpdateModels);
            eventsAddedEvent?.Invoke(toAddModel);
            eventsRemovedEvent?.Invoke(syncRequirement.Delete);
        }

        private XmlSportsModel Get(string fileName)
        {
            using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var xsw = new XmlSerializer(typeof(XmlSportsModel));
                var stream = new StreamReader(fs, Encoding.UTF8);
                var config = (XmlSportsModel)xsw.Deserialize(new XmlTextReader(stream));
                return config;
            }
        }
    }
}
