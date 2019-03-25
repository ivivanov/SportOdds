using AutoMapper;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class OddProfile : Profile
    {
        public OddProfile()
        {
            CreateMap<OddModel, Odd>();
            CreateMap<Odd, OddModel>();
        }
    }
}
