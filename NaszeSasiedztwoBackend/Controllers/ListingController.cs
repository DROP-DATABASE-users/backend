using Microsoft.AspNetCore.Mvc;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Services;

namespace NaszeSasiedztwoBackend.Controllers;

[Route("api/listing")]
[ApiController]
public class ListingController : ControllerBase
{
	private readonly IListingService _listingService;

	public ListingController(IListingService listingService)
	{
		_listingService = listingService;
	}

	[HttpGet]
	public ActionResult<List<ListingDto>> GetAllListings()
	{
		return Ok(_listingService.GetAllListings());
	}
}