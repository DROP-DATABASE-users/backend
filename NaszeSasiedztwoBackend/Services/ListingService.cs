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
		var listings = _context.Listings.Include(x => x.Author);

		return _mapper.Map<List<ListingDto>>(listings);
	}

	public int CreateListing(CreateListingDto dto)
	{
		var listing = _mapper.Map<Listing>(dto);

		_context.Listings.Add(listing);

		listing.Author = _context.Users.FirstOrDefault(x => x.Id == dto.AuthorId);

		_context.SaveChanges();

		return listing.Id;
	}

	public void DeleteListing(int id)
	{
		var listing = GetListingById(id);

		_context.Remove(listing);

		_context.SaveChanges();
	}

	public void UpdateListing(int id, EditListingDto dto)
	{
		var listing = GetListingById(id);

		listing.Title = dto.Title;
		listing.Description = dto.Description;
		listing.CoordinatesX = dto.CoordinatesX;
		listing.CoordinatesY = dto.CoordinatesY;
		listing.Author = GetUserById(dto.AuthorId);

		if (dto.ContractorId != 0) listing.Contractor = GetUserById(dto.ContractorId);

		_context.SaveChanges();
	}

	private Listing GetListingById(int id)
	{
		var listing = _context.Listings.FirstOrDefault(x => x.Id == id);

		if (listing is null) throw new ArgumentNullException($"Listing with id: '{id}' not found");

		return listing;
	}

	private User GetUserById(int id)
	{
		var user = _context.Users.FirstOrDefault(x => x.Id == id);

		if (user is null) throw new ArgumentNullException($"User with id: '{id}' not found");

		return user;
	}
}