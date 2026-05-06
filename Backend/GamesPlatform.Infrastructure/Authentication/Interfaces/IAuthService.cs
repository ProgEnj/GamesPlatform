using GamesPlatform.Infrastructure.Authentication.DTOs;
using GamesPlatform.Infrastructure.ErrorHandling;
using GamesPlatform.Infrastructure.Persistance.Identity;

namespace GamesPlatform.Infrastructure.Authentication.Interfaces;

public interface IAuthService
{
    Task<Result> RegisterAsync(UserRegisterRequestDTO request);
    Task<Result<UserLoginResponseDTO>> LoginAsync(UserLoginRequestDTO request);
    Task<Result> ConfirmEmailAsync(string userId, string code);
    Task<Result> SendConfirmationEmailAsync(ApplicationUser user);
    Task<Result> ForgotPasswordEmailAsync(ForgotPasswordRequestDTO request);
    Task<Result> ResetPasswordAsync(ResetPasswordRequestDTO request);
    Task<Result<string>> RefreshAccessTokenAsync();
    Task<Result> LogoutUserAsync();
}