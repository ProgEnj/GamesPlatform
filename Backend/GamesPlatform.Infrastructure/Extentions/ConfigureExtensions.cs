using System.Text;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GamesPlatform.Infrastructure.Extentions;

public static class ConfigureExtension
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            
            .AddDefaultTokenProviders();
    }

    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAuthentication(o =>
            {
                o.RequireAuthenticatedSignIn = false;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = configuration["JwtSettings:Issuer"] != null,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = configuration["JwwtSettings:Audience"] != null,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Token"])),
                    ValidateIssuerSigningKey = true
                };
            })
            .AddCookie("refreshTokenCookie", o =>
            {
                o.Cookie.Name = "refreshToken";
                o.Cookie.HttpOnly = true;
                o.Cookie.SameSite = SameSiteMode.Strict;
                o.Cookie.Path = "auth/refreshaccess";
                o.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
    }
    
    public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(o =>
        {
            o.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            o.AddPolicy("RefreshTokenPolicy", policy => policy.RequireClaim("refreshToken"));
        });
    }
}