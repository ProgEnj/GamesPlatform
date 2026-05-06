namespace GamesPlatform.Infrastructure.Authentication.DTOs;

public class ForgotPasswordRequestDTO
{
    public string Email { get; }
    
    public ForgotPasswordRequestDTO(string email)
    {
        Email = email;
    }
}