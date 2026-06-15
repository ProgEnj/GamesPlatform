namespace GamesPlatform.Infrastructure.Authentication.DTOs;

public class UserLoginResponseDTO
{
    public string UserName { get; }
    public string Email { get; }
    public string Token { get; }

    public UserLoginResponseDTO(string userName, string email, string token)
    {
        UserName = userName;
        Email = email;
        Token = token;
    }
}
