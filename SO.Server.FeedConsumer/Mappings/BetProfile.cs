﻿using AutoMapper;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Mappings
{
    public class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<BetModel, Bet>();
            CreateMap<Bet, BetModel>();
        }
    }
}
