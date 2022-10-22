using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NaszeSasiedztwoBackend.Authorization;
using NaszeSasiedztwoBackend.Entities;
using NaszeSasiedztwoBackend.Services;
using NaszeSasiedztwoBackend.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = "Bearer";
	options.DefaultScheme = "Bearer";
	options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
	cfg.RequireHttpsMetadata = false;
	cfg.SaveToken = true;
	cfg.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = authenticationSettings.JwtIssuer,
		ValidAudience = authenticationSettings.JwtIssuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
	};
});

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

builder.Services.AddDbContext<NaszeSasiedztwoDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("HotelDatabase")));
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
app.UseCors(b =>
{
	b.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nasze Sasiedztwo");
});

app.UseAuthorization();

app.MapControllers();

app.Run();