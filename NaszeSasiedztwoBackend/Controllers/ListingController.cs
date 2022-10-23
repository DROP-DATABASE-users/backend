using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Services;
using NaszeSasiedztwoBackend.Utils;
using NaszeSasiedztwoBackend.Utils.Exceptions;

namespace NaszeSasiedztwoBackend.Controllers;

[Route("api/listing")]
[ApiController]
[Authorize]
public class ListingController : ControllerBase
{
	private readonly IListingService _listingService;

	public ListingController(IListingService listingService)
	{
		_listingService = listingService;
	}

	[HttpGet]
	[Route("{region}")]
	public ActionResult<List<ListingDto>> GetAllListings([FromRoute] Region region)
	{
		return Ok(_listingService.GetAllListings(region));
	}

	[HttpPost]
	public ActionResult CreateListing([FromBody] CreateListingDto dto)
	{
		try
		{
			var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
		catch (ForbiddenException ex)
		{
			return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
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
		catch (ForbiddenException ex)
		{
			return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
		}
		catch (Exception e)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
		}
	}

	[HttpPatch]
	[Route("{id}")]
	public ActionResult AddContractor([FromRoute] int id)
	{
		try
		{
			_listingService.AddContractor(id);
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