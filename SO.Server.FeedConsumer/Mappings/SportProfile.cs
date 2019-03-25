using AutoMapper;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class SportProfile : Profile
    {
        public SportProfile()
        {
            CreateMap<SportModel, Sport>();
            CreateMap<Sport, SportModel>();
        }
    }
}
