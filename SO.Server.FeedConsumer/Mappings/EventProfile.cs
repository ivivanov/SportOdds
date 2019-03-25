using AutoMapper;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventModel, Event>();
            CreateMap<Event, EventModel>();
        }
    }
}
