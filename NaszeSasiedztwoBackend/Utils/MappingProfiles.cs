using AutoMapper;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Utils;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Listing, ListingDto>();
		CreateMap<User, UserDto>();
		CreateMap<ListingUser, ListingUserDto>();
		CreateMap<CreateListingDto, Listing>();
	}
}