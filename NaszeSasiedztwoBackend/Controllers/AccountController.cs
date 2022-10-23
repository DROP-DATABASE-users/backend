using System.Security.Claims;
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

	[Route("login")]
	[HttpPost]
	public ActionResult Login([FromBody] LoginDto dto)
	{
		try
		{
			string token = _accountService.GenerateJwt(dto);
			return Ok(token);
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

	[Route("user")]
	[HttpGet]
	public ActionResult<UserDto> GetUser()
	{
		try
		{
			var userId = int.Parse(User.FindFirst(t => t.Type == ClaimTypes.NameIdentifier).Value);
			return Ok(_accountService.GetUser(userId));

		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}