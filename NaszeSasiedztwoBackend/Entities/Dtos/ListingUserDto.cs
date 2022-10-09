namespace NaszeSasiedztwoBackend.Entities.Dtos;

public class ListingUserDto
{
	public int ListingId { get; set; }
	public int UserId { get; set; }
	public virtual UserDto User { get; set; }
}