using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services;

public class ListingService : IListingService
{
	private readonly NaszeSasiedztwoDbContext _context;
	private readonly IMapper _mapper;

	public ListingService(NaszeSasiedztwoDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public List<ListingDto> GetAllListings()
	{
		var listings = _context.Listings.Include(x => x.Users).ThenInclude(x => x.User).ToList();

		return _mapper.Map<List<ListingDto>>(listings);
	}

	public int CreateListing(CreateListingDto dto)
	{
		var listing = _mapper.Map<Listing>(dto);

		_context.Listings.Add(listing);
		_context.SaveChanges();

		listing.Users = new List<ListingUser>
		{
			new ListingUser
			{
				ListingId = listing.Id,
				UserId = listing.AuthorId,
			}
		};
		_context.SaveChanges();
		return listing.Id;
	}
}