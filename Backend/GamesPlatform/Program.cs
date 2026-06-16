using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using GamesPlatform.Infrastructure.Authentication;
using GamesPlatform.Infrastructure.Authentication.Implementation;
using GamesPlatform.Infrastructure.Authentication.Interfaces;
using GamesPlatform.Infrastructure.Extentions;
using GamesPlatform.Infrastructure.IGDB;
using GamesPlatform.Application.Features.Game.Implementation;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Features.Profile.Implementation;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Features.Reviews.Implementation;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:Default"]));
builder.Services.AddIdentity(configuration);
builder.Services.AddAuthentication(configuration);
builder.Services.AddAuthorization(configuration);

builder.Services.AddEndpointsApiExplorer();
// TODO: Separate extention method
builder.Services.AddSwaggerGen(o =>
{
	o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		Name = "Authorization"
	});
	o.AddSecurityRequirement(x => new OpenApiSecurityRequirement()
	{
		[new OpenApiSecuritySchemeReference("Bearer", x)] = []
	});
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailSenderDummy>();

// TODO: Add with extention method per feature
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
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
//app.MapSwagger();

app.Run();