using System.Net;
using Microsoft.AspNetCore.Mvc;
using NaszeSasiedztwoBackend.Entities;
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

	[HttpPost]
	public ActionResult CreateListing([FromBody] CreateListingDto dto)
	{
		try
		{
			var id = _listingService.CreateListing(dto);
			return Created($"api/listing/{id}", null);
		}
		catch (Exception ex)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
		}
	}

	[HttpDelete]
	[Route("{id}")]
	public ActionResult DeleteListing([FromRoute] int id)
	{
		try
		{
			_listingService.DeleteListing(id);
			return NoContent();
		}
		catch (ArgumentNullException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception e)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
		}
	}

	[HttpPut]
	[Route("{id}")]
	public ActionResult<Listing> UpdateListing([FromRoute] int id, [FromBody] EditListingDto dto)
	{
		try
		{
			_listingService.UpdateListing(id, dto);
			return Ok();

		}
		catch (ArgumentNullException ex)
		{
			return NotFound(ex.Message);
		}
		catch (Exception e)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
		}
	}
}