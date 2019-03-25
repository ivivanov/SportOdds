using AutoMapper;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class BaseModelProfile : Profile
    {
        public BaseModelProfile()
        {
            CreateMap<BaseModel, Sport>();
            CreateMap<BaseModel, Event>();
            CreateMap<BaseModel, Match>();
            CreateMap<BaseModel, Bet>();
            CreateMap<BaseModel, Odd>();
        }
    }
}
