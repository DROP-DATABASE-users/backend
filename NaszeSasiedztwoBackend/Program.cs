using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Services;
using NaszeSasiedztwoBackend.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NaszeSasiedztwoDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("HotelDatabase")));
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IRelationHelper, RelationHelper>();

builder.Services.AddScoped<IListingService, ListingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();