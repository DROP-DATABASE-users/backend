using NaszeSasiedztwoBackend.Entities;

namespace NaszeSasiedztwoBackend.Utils;

public class DbSeeder
{
	private readonly NaszeSasiedztwoDbContext _context;

	public DbSeeder(NaszeSasiedztwoDbContext context)
	{
		_context = context;
	}

	public void Seed()
	{
		if (!_context.Users.Any())
			_context.Users.AddRange(GetUsers());
		_context.SaveChanges();
		if (!_context.Listings.Any())
			_context.Listings.AddRange(GetListings());
		_context.SaveChanges();
	}

	private List<Listing> GetListings()
	{
		return new List<Listing>
		{
			new()
			{
				Title = "Potrzebuje pomocy",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "51.189845",
				CoordinatesY = "20.405785",
				Author = _context.Users.FirstOrDefault(x => x.Id == 1),
			},
		};
	}

	private List<User> GetUsers()
	{
		return new List<User>
		{
			new()
			{
				Name = "Tomasz",
				LastName = "Kowalski",
				Description = "Hej jestem Tomek, chętnie pomogę",
			},
			new()
			{
				Name = "Stefan",
				LastName = "Bezręki",
				Description = "Hej jestem Stefan, potrzebuję pomocy",
			},
		};
	}
}