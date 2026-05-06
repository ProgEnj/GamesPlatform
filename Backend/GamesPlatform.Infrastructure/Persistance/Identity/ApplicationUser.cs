using Microsoft.AspNetCore.Identity;

namespace GamesPlatform.Infrastructure.Persistance.Identity;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}