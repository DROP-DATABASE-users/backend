using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services;

public interface IAccountService
{
	int RegisterUser(RegisterUserDto dto);
	string GenerateJwt(LoginDto dto);
	UserDto GetUser(int id);
}