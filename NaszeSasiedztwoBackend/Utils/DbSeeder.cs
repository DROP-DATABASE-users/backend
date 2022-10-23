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
				Region = Region.świętokrzyskie,
			},
			new()
			{
				Title = "Pomoc bardzo potrzebna",
				Description = "Cześć! Potrzebuje pomocy z wniesieniem ",
				CoordinatesX = "52.247033",
				CoordinatesY = "21.010242",
				Author = _context.Users.FirstOrDefault(x=>x.Id==2),
				Region = Region.mazowieckie,
			},
			new()
			{
				Title = "Pomoc Kupno Ziemnaków",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "50.041277",
				CoordinatesY = "19.975410",
				Author = _context.Users.FirstOrDefault(x => x.Id == 3),
				Region = Region.małopolskie,
			},
			new()
			{
				Title = "Pomoc Kupno Buraków",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "49.685003",
				CoordinatesY = "21.775102",
				Author = _context.Users.FirstOrDefault(x => x.Id == 1),
				Region = Region.podkarpackie,
			},
			new()
			{
				Title = "Pomoc Kupno Jabłek",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "51.202454",
				CoordinatesY = "22.559393",
				Author = _context.Users.FirstOrDefault(x => x.Id == 2),
				Region = Region.lubelskie,
			},
			new()
			{
				Title = "Pomoc Kupno Ziemniaków",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "53.140625",
				CoordinatesY = "23.114917",
				Author = _context.Users.FirstOrDefault(x => x.Id == 3),
				Region = Region.podlaskie,
			},
			new()
			{
				Title = "Pomoc Kupno Owoców",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "53.762433",
				CoordinatesY = "20.446681",
				Author = _context.Users.FirstOrDefault(x => x.Id == 1),
				Region = Region.warmińskoMazurskie,
			},
			new()
			{
				Title = "Pomoc Kupno Warzyw",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "54.327968",
				CoordinatesY = "18.631756",
				Author = _context.Users.FirstOrDefault(x => x.Id == 2),
				Region = Region.pomorskie,
			},
			new()
			{
				Title = "Pomoc Kupno Warzyw",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "53.240860",
				CoordinatesY = "18.159186",
				Author = _context.Users.FirstOrDefault(x => x.Id == 3),
				Region = Region.kujawskoPomorskie,
			},
			new()
			{
				Title = "Pomoc Kupno Owoców",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "53.339052",
				CoordinatesY = "15.027053",
				Author = _context.Users.FirstOrDefault(x => x.Id == 1),
				Region = Region.zachodniopomorskie,
			},
			new()
			{
				Title = "Pomoc Kupno Owoców",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "51.644899",
				CoordinatesY = "17.811950",
				Author = _context.Users.FirstOrDefault(x => x.Id == 2),
				Region = Region.wielkopolskie,
			},
			new()
			{
				Title = "Pomoc Kupno Warzyw",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "51.079157",
				CoordinatesY =  "17.002195",
				Author = _context.Users.FirstOrDefault(x => x.Id == 3),
				Region = Region.dolnośląskie,
			},
			new()
			{
				Title = "Pomoc Kupno Owoców",
				Description = "Hej potrzebuje pomocy, potrzebuje zakupów",
				CoordinatesX = "50.214416",
				CoordinatesY =  "18.844802",
				Author = _context.Users.FirstOrDefault(x => x.Id == 1),
				Region = Region.śląskie,
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
				UserName = "tomko",
				Description = "Hej jestem Tomek, mieszkam sam, chętnie pomogę",
			},
			new()
			{
				Name = "Stefan",
				LastName = "Należny",
				UserName = "stefko",
				Description = "Hej jestem Stefan, potrzebuję pomocy",
			},
			new()
			{
				Name = "Andrzej",
				LastName = "Kowalewski",
				UserName = "andrzejko",
				Description = "Hej jestem Andrzej, potrzebuję pomocy",
			},
		};
	}
}