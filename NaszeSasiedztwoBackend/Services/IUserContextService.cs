using System.Security.Claims;

namespace NaszeSasiedztwoBackend.Services;

public interface IUserContextService
{
	ClaimsPrincipal User { get; }
	int? GetUserId { get; }
}