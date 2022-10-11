using NaszeSasiedztwoBackend.Utils;

namespace NaszeSasiedztwoBackend.Entities;

public class Listing
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string CoordinatesX { get; set; }
	public string CoordinatesY { get; set; }
	public Region Region { get; set; }
	public User Author { get; set; }
	public User? Contractor { get; set; }
}