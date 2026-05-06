using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using GamesPlatform.Infrastructure.Authentication;
using GamesPlatform.Infrastructure.Authentication.Implementation;
using GamesPlatform.Infrastructure.Authentication.Interfaces;
using GamesPlatform.Infrastructure.Extentions;
using GamesPlatform.Infrastructure.IGDB;
using GamesPlatform.Infrastructure.Persistance;
using GamesPlatform.Infrastructure.Persistance.Identity;
using GamesPlatform.UseCases.Features.Game.Implementation;
using GamesPlatform.UseCases.Features.Game.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:Default"]));
builder.Services.AddIdentity(configuration);
builder.Services.AddAuthentication(configuration);
builder.Services.AddAuthorization(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailSenderDummy>();

// TODO: Add with extention method per feature
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddSingleton<IIGDBService, IGDBService>();

builder.Services.AddHealthChecks()
	.AddNpgSql(configuration["ConnectionStrings:Default"], healthQuery: "select 1", name: "PostgreSQL", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" });

// var context = builder.Services.BuildServiceProvider().GetService<ApplicationDbContext>();
// await context.Database.MigrateAsync();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/api/health");
app.MapSwagger();

app.Run();