using AutoMapper;
using SO.Server.Data.Entities;
using SO.Server.FeedConsumer.DTOs;

namespace SO.Server.FeedConsumer.Mappings {
	public class OddProfile : Profile {
		public OddProfile() {
			CreateMap<OddDto, Odd>();
		}
	}
}
