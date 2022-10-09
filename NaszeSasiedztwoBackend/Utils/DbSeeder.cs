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
		if (!_context.ListingsUsers.Any())
			_context.ListingsUsers.AddRange(GetListingUsers());
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
				AuthorId = 2,
				ContractorId = 1,
			},
		};
	}

	private List<ListingUser> GetListingUsers()
	{
		var users = _context.Users.ToList();
		var user1 = users[0];
		var user2 = users[1];

		var listing = _context.Listings.FirstOrDefault();

		var listingUser = new ListingUser
		{
			ListingId = listing.Id,
			UserId = user1.Id,
		};

		var listingsUser2 = new ListingUser
		{
			ListingId = listing.Id,
			UserId = user2.Id,
		};

		return new List<ListingUser> {listingUser, listingsUser2};
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