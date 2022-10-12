using System.Security.Claims;
using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services;

public interface IListingService
{
	List<ListingDto> GetAllListings();
	int CreateListing(CreateListingDto dto, int id);
	void DeleteListing(int id, ClaimsPrincipal user);
	void UpdateListing(int id, EditListingDto dto, ClaimsPrincipal user);
}