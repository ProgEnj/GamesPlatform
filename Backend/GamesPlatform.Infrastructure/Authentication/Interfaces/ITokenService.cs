using GamesPlatform.Infrastructure.Persistance.Identity;

namespace GamesPlatform.Infrastructure.Authentication.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);

    string GenerateRefreshToken();
}