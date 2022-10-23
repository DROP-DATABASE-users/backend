using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NaszeSasiedztwoBackend.Authorization;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Utils;
using NaszeSasiedztwoBackend.Utils.Exceptions;

namespace NaszeSasiedztwoBackend.Services;

public class ListingService : IListingService
{
	private readonly NaszeSasiedztwoDbContext _context;
	private readonly IMapper _mapper;
	private readonly IAuthorizationService _authorizationService;
	private readonly IUserContextService _userContextService;

	public ListingService(NaszeSasiedztwoDbContext context, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
	{
		_context = context;
		_mapper = mapper;
		_authorizationService = authorizationService;
		_userContextService = userContextService;
	}

	public List<ListingDto> GetAllListings(Region region)
	{
		var listings = _context.Listings.Include(x => x.Author).Where(x => x.Region == region);

		return _mapper.Map<List<ListingDto>>(listings);
	}

	public int CreateListing(CreateListingDto dto)
	{
		var listing = _mapper.Map<Listing>(dto);

		_context.Listings.Add(listing);

		listing.Author = GetUserById(_userContextService.GetUserId);

		_context.SaveChanges();

		return listing.Id;
	}

	public void DeleteListing(int id)
	{
		var listing = GetListingById(id);

		var authorizationResult = _authorizationService
			.AuthorizeAsync(_userContextService.User, listing, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

		if (!authorizationResult.Succeeded)
		{
			throw new ForbiddenException("Unathorized");
		}

		_context.Remove(listing);

		_context.SaveChanges();
	}

	public void UpdateListing(int id, EditListingDto dto)
	{

		var listing = GetListingById(id);

		var authorizationResult = _authorizationService
			.AuthorizeAsync(_userContextService.User, listing, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

		if (!authorizationResult.Succeeded)
		{
			throw new ForbiddenException("Unathorized");
		}

		listing.Title = dto.Title;
		listing.Description = dto.Description;
		listing.CoordinatesX = dto.CoordinatesX;
		listing.CoordinatesY = dto.CoordinatesY;

		if (dto.ContractorId != 0) listing.Contractor = GetUserById(dto.ContractorId);

		_context.SaveChanges();
	}

	private Listing GetListingById(int id)
	{
		var listing = _context.Listings.Include(x => x.Author).FirstOrDefault(x => x.Id == id);

		if (listing is null) throw new ArgumentNullException($"Listing with id: '{id}' not found");

		return listing;
	}

	private User GetUserById(int? id)
	{
		var user = _context.Users.FirstOrDefault(x => x.Id == id);

		if (user is null) throw new ArgumentNullException($"User with id: '{id}' not found");

		return user;
	}
}