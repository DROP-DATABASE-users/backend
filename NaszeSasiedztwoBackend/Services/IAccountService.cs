using NaszeSasiedztwoBackend.Entities.Dtos;

namespace NaszeSasiedztwoBackend.Services;

public interface IAccountService
{
	int RegisterUser(RegisterUserDto dto);
}