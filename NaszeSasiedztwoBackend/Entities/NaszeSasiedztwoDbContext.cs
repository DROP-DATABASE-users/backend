using Microsoft.EntityFrameworkCore;

namespace NaszeSasiedztwoBackend.Entities;

public class NaszeSasiedztwoDbContext : DbContext
{
	public NaszeSasiedztwoDbContext(DbContextOptions<NaszeSasiedztwoDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Listing> Listings { get; set; }
}