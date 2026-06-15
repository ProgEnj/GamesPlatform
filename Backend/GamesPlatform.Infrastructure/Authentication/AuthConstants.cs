namespace GamesPlatform.Infrastructure.Authentication;

public static class AuthConstants
{
    //TODO: Maybe make this as folder with Roles, Claims, policies
    public static readonly string RefreshTokenCookieScheme = "RefreshTokenCookie";
    public static readonly string RefreshTokenClaim = "RefreshToken";
    public static readonly string JwtTokenScheme = "JwtToken";

    public static readonly string AdminPolicy = "AdminPolicy";
    public static readonly string AdminRole = "AdminRole";
}