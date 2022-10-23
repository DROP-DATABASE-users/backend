using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Entities.Dtos;
using NaszeSasiedztwoBackend.Utils;

namespace NaszeSasiedztwoBackend.Services;

public class AccountService : IAccountService
{
	private readonly NaszeSasiedztwoDbContext _context;
	private readonly IMapper _mapper;
	private readonly AuthenticationSettings _authenticationSettings;
	private readonly PasswordHasher<User> _passwordHasher;

	public AccountService(NaszeSasiedztwoDbContext context, IMapper mapper, AuthenticationSettings authenticationSettings)
	{
		_context = context;
		_mapper = mapper;
		_authenticationSettings = authenticationSettings;
		_passwordHasher = new PasswordHasher<User>();
	}

	public int RegisterUser(RegisterUserDto dto)
	{
		if (_context.Users.Any(x => x.UserName == dto.UserName))
			throw new ArgumentException("Name is already in use");

		var user = _mapper.Map<User>(dto);
		user.HashedPassword = _passwordHasher.HashPassword(user, dto.Password);

		_context.Users.Add(user);
		_context.SaveChanges();

		return user.Id;
	}

	public string GenerateJwt(LoginDto dto)
	{
		var user = _context.Users.FirstOrDefault(x => x.UserName == dto.Username);

		if (user == null)
		{
			throw new ArgumentException("Invalid username or password");
		}

		var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);

		if (result == PasswordVerificationResult.Failed)
		{
			throw new ArgumentException("Invalid username or password");
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

		var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims,
			expires: expires, signingCredentials: cred);

		var tokenHandler = new JwtSecurityTokenHandler();

		return tokenHandler.WriteToken(token);
	}

	public UserDto GetUser(int id)
	{
		var user = _context.Users.FirstOrDefault(x => x.Id == id);

		if (user == null)
		{
			throw new ArgumentNullException();
		}

		return _mapper.Map<UserDto>(user);
	}
}