using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Utils;

namespace NaszeSasiedztwoBackend.Services;

public class ListingService : IListingService
{
	private readonly NaszeSasiedztwoDbContext _context;
	private readonly IMapper _mapper;
	private readonly IRelationHelper _relationHelper;

	public ListingService(NaszeSasiedztwoDbContext context, IMapper mapper, IRelationHelper relationHelper)
	{
		_context = context;
		_mapper = mapper;
		_relationHelper = relationHelper;
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
			new()
			{
				ListingId = listing.Id,
				UserId = listing.AuthorId,
			}
		};
		_context.SaveChanges();
		return listing.Id;
	}

	public void DeleteListing(int id)
	{
		var listing = GetListingById(id);

		var listingUsers = _context.ListingsUsers.Where(x => x.ListingId == listing.Id);
		_context.ListingsUsers.RemoveRange(listingUsers);
		_context.Remove(listing);

		_context.SaveChanges();
	}

	public void UpdateListing(int id, EditListingDto dto)
	{
		var listing = GetListingById(id);

		listing.Title = dto.Title;
		listing.AuthorId = dto.AuthorId;
		listing.ContractorId = dto.ContractorId;
		listing.Description = dto.Description;
		listing.CoordinatesX = dto.CoordinatesX;
		listing.CoordinatesY = dto.CoordinatesY;

		_relationHelper.CreateManyToManyRelationForListingUser(listing);

		_context.SaveChanges();
	}

	private Listing GetListingById(int id)
	{
		var listing = _context.Listings.FirstOrDefault(x => x.Id == id);

		if (listing is null) throw new ArgumentNullException($"Listing with id: '{id}' not found");

		return listing;
	}
}