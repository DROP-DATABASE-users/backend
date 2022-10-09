namespace NaszeSasiedztwoBackend.Entities;

public class ListingUser
{
	public int ListingId { get; set; }
	public virtual Listing Listing { get; set; }

	public int UserId { get; set; }
	public virtual User User { get; set; }
}