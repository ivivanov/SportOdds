using AutoMapper;
using SO.Server.Data.Entities;
using SO.Server.FeedConsumer.DTOs;

namespace SO.Server.FeedConsumer.Mappings {
	public class SportProfile : Profile {
		public SportProfile() {
			CreateMap<SportDto, Sport>();
		}
	}
}
