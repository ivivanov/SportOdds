using AutoMapper;
using SO.Server.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchModel, Match>();
            CreateMap<Match, MatchModel>();
        }
    }
}
