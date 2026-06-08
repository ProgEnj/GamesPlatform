using GamesPlatform.Core.Model.User;
using Microsoft.AspNetCore.Identity;

namespace GamesPlatform.Application.Persistance.Identity;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
    public UserProfile UserProfile { get; set; }
}