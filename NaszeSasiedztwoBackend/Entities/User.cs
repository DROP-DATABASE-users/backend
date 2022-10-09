namespace NaszeSasiedztwoBackend.Entities;

public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string LastName { get; set; }
	public string Description { get; set; }

	public virtual List<Listing> Listing { get; set; }
}