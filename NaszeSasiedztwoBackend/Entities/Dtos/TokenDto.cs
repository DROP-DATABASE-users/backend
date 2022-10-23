namespace NaszeSasiedztwoBackend.Entities.Dtos;

public class TokenDto
{
	public string JWTToken { get; set; }

	public TokenDto(string token)
	{
		JWTToken = token;
	}
}