using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SO.Server.Data;
using SO.Server.Data.Entities;
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
        //public delegate void MatchChangedHandler(IEnumerable<int> changedMatches);
        //public event MatchChangedHandler matchChangedEvent;

        private readonly IUnitOfWork _uow;
        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<Sport> _sportRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly Sport _currentSport;

        public FeedConsumerJob(IMapper mapper, IUnitOfWork uow)
        {
            _uow = uow;
            //_matchRepository = uow.GetRepository<Match>();
            _sportRepository = uow.GetRepository<Sport>();
            //_eventRepository = uow.GetRepository<Event>();
            _mapper = mapper;
            _currentSport = _sportRepository
                    .FirstOrDefault(x => x.Id == 1165, //Just for POC 
                            include: x =>
                                x.Include(sp => sp.Events)
                                .ThenInclude(ev => ev.Matches)
                                .ThenInclude(ma => ma.Bets)
                                .ThenInclude(bet => bet.Odds));
        }

        public void Fetch()
        {
            var updatedSport = Get("soccer.xml");
            //var updatedSport = _mapper.Map<Sport>(updated.Sport);
            var currentSport = _mapper.Map<SportModel>(_currentSport);

            if (_currentSport == null)// Initial DB seed
            {
                _sportRepository.Add(_mapper.Map<Sport>(updatedSport.Sport));
            }
            else
            {
                var syncResult = Sync<Event, EventModel>(currentSport.Events, updatedSport.Sport.Events);


                var genericRepository = _uow.GetRepository<Event>();
                var toAdd = _mapper.Map<IEnumerable<Event>>(updatedSport.Sport.Events.Where(x => syncResult.Add.Contains(x.Id)));
                var toDelete = _mapper.Map<IEnumerable<Event>>(currentSport.Events.Where(x => syncResult.Delete.Contains(x.Id)));
                var toUpdate = _mapper.Map<IEnumerable<Event>>(currentSport.Events.Where(x => syncResult.Update.Contains(x.Id)));
                genericRepository.Add(toAdd);
                genericRepository.Delete(toDelete);
                genericRepository.Update(toUpdate);
                //Sync Events
                //var currentMatches = _mapper.Map<IEnumerable<MatchModel>>(_currentSport.Events.SelectMany(x => x.Matches));
                //var updatedMatches = updated.Sport.Events.SelectMany(x => x.Matches);

                //var update = DiffChecker.ToUpdateIDs(currentMatches, updatedMatches);
                //var delete = DiffChecker.ToDeleteIDs(currentMatches, updatedMatches);
                //var add = DiffChecker.ToAddIDs(currentMatches, updatedMatches);

                //_matchRepository.Delete(delete.Select(id => new Match() { Id = id }));
            }

            //matchChangedEvent(update);
            //var deleteEvents = mapper.Map<IEnumerable<Event>>(delete);
            //eventsRepo.Delete(deleteEvents);
            //update
            //var update = currentXml.Sport.Events.Intersect(newXml.Sport.Events);
            //add
            //var add = newXml.Sport.Events.Except(currentXml.Sport.Events);
            //var addEvents = mapper.Map<IEnumerable<Event>>(add);
            //eventsRepo.Add(addEvents);
            _uow.SaveChanges();
        }


        private SyncResult Sync<Z, T>(IEnumerable<T> oldCollection, IEnumerable<T> newCollection)
            where Z : class, IEntity
            where T : IBaseModel
        {
            return new SyncResult()
            {
                Add = DiffChecker.ToAddIDs(oldCollection, newCollection),
                Update = DiffChecker.ToUpdateIDs(oldCollection, newCollection),
                Delete = DiffChecker.ToDeleteIDs(oldCollection, newCollection)
            };
        }


        private XmlSportsModel Get(string fileName)
        {
            var xsw = new XmlSerializer(typeof(XmlSportsModel));
            var fs = new FileStream(fileName, FileMode.Open);
            var stream = new StreamReader(fs, Encoding.UTF8);
            var config = (XmlSportsModel)xsw.Deserialize(new XmlTextReader(stream));
            return config;
        }
    }
}
