using Microsoft.EntityFrameworkCore;
using NaszeSasiedztwoBackend.Utils;

namespace NaszeSasiedztwoBackend.Entities;

public class NaszeSasiedztwoDbContext : DbContext
{
	public NaszeSasiedztwoDbContext(DbContextOptions<NaszeSasiedztwoDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Listing> Listings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Listing>()
			.Property(x => x.Region)
			.HasConversion<string>();
	}
}