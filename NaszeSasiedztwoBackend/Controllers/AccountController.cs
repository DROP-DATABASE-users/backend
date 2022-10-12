using Microsoft.AspNetCore.Mvc;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Services;

namespace NaszeSasiedztwoBackend.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[Route("register")]
	[HttpPost]
	public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
	{
		try
		{
			var id = _accountService.RegisterUser(dto);
			return Created($"/api/account/{id}", null);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}