using System.Security.Claims;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Utils;

namespace NaszeSasiedztwoBackend.Services;

public interface IListingService
{
	List<ListingDto> GetAllListings(Region region);
	int CreateListing(CreateListingDto dto);
	void DeleteListing(int id);
	void UpdateListing(int id, EditListingDto dto);
	void AddContractor(int id);
}