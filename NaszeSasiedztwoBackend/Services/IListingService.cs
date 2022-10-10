using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services;

public interface IListingService
{
	List<ListingDto> GetAllListings();
	int CreateListing(CreateListingDto dto);
}