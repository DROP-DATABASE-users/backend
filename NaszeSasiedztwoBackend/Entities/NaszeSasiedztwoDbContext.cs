using Microsoft.EntityFrameworkCore;

namespace NaszeSasiedztwoBackend.Entities;

public class NaszeSasiedztwoDbContext : DbContext
{
	public NaszeSasiedztwoDbContext(DbContextOptions<NaszeSasiedztwoDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Listing> Listings { get; set; }
	public DbSet<ListingUser> ListingsUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ListingUser>().HasKey(i => new {i.ListingId, i.UserId});
	}
}