using System.Security.Claims;
using System.Text;
using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Infrastructure.Authentication.DTOs;
using GamesPlatform.Infrastructure.Authentication.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamesPlatform.Infrastructure.Authentication.Implementation;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;
    private readonly IEmailSender<ApplicationUser> _emailSender;
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserProfileService _userProfileServiceService;

    private readonly string confirmEmailEndpointName = "auth/confirmemail";
    private readonly string ResetPasswordEdpointName = "auth/resetpassword";

    public AuthService(LinkGenerator linkGenerator, IConfiguration configuration, 
        UserManager<ApplicationUser> userManager, ITokenService tokenService, ApplicationDbContext context, 
        IEmailSender<ApplicationUser> emailSender, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor,
        IUserProfileService userProfileServiceService)
    {
        _linkGenerator = linkGenerator;
        _configuration = configuration;
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
        _emailSender = emailSender;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _userProfileServiceService = userProfileServiceService;
    }

    public async Task<Result> RegisterAsync(UserRegisterRequestDTO request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            return Result.Failure(AuthenticationErrors.UserAlreadyExist);
        }

        if (await _userManager.FindByNameAsync(request.UserName) != null)
        {
            return Result.Failure(AuthenticationErrors.UserAlreadyExist);
        }

        var userProfile = await _userProfileServiceService.CreateNewProfileAsync(request.UserName);

        var result = await _userManager.CreateAsync(new ApplicationUser(){UserName = request.UserName, 
            Email = request.Email, UserProfile = userProfile.Value}, request.Password);
        
        if (!result.Succeeded)
        {
            return Result.Failure(AuthenticationErrors.GenericError);
        }
        
        //await SendConfirmationEmailAsync(newUser);
        return Result.Success();
    }
    
    public async Task<Result<UserLoginResponseDTO>> LoginAsync(UserLoginRequestDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure<UserLoginResponseDTO>(AuthenticationErrors.UserNotFound);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Failure<UserLoginResponseDTO>(AuthenticationErrors.InvalidLogin);
        }

        var token = await _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result.Failure<UserLoginResponseDTO>(AuthenticationErrors.GenericError);
        }
        
        var claims = new List<Claim>() { new Claim("refreshToken", refreshToken) };
        await _httpContextAccessor.HttpContext.SignInAsync(
            "refreshTokenCookie", new ClaimsPrincipal(new ClaimsIdentity(claims, "refreshToken")));

        var result = new UserLoginResponseDTO(user.UserName, user.Email, token, refreshToken);
        return Result.Success(result);
    }

    public async Task<Result> LogoutUserAsync()
    {
        string? refreshToken = _httpContextAccessor.HttpContext.User.FindFirstValue("refreshToken");
        if(refreshToken == null)
        {
            return Result.Failure(AuthenticationErrors.WrongToken);
        }
        
        var user = await _userManager.Users.FirstAsync(u => u.RefreshToken == refreshToken);
        if (user == null)
        {
            return Result.Failure(AuthenticationErrors.WrongToken);
        }
        
        user.RefreshToken = null;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result.Failure(AuthenticationErrors.GenericError);
        }
        
        await _httpContextAccessor.HttpContext.SignOutAsync("refreshTokenCookie");
        return Result.Success();
    }

    public async Task<Result> SendConfirmationEmailAsync(ApplicationUser user)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var routeValues = new RouteValueDictionary()
        {
            ["userId"] = user.Id,
            ["code"] = code,
        };

        var confirmEmailURL = _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, confirmEmailEndpointName, routeValues);
        await _emailSender.SendConfirmationLinkAsync(user, user.Email, confirmEmailURL);
        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure(AuthenticationErrors.UserNotFound);
        }
        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        
        if(!result.Succeeded)
        {
            return Result.Failure(AuthenticationErrors.ConfirmationEmail);
        }
        
        return Result.Success();
    }

    public async Task<Result> ForgotPasswordEmailAsync(ForgotPasswordRequestDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure(AuthenticationErrors.UserNotFound);
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        
        var routeValues = new RouteValueDictionary()
        {
            ["userId"] = user.Id,
            ["code"] = code,
        };

        var confirmEmailURL = _linkGenerator.GetUriByName(_httpContextAccessor.HttpContext, ResetPasswordEdpointName, routeValues);
        await _emailSender.SendPasswordResetLinkAsync(user, user.Email, code);
        return Result.Success();
    }
    
    public async Task<Result> ResetPasswordAsync(ResetPasswordRequestDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure(AuthenticationErrors.UserNotFound);
        }
        if (!user.EmailConfirmed)
        {
            return Result.Failure(AuthenticationErrors.ConfirmedEmail);
        }
        
        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.ResetCode));
        var result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
        
        if (!result.Succeeded)
        {
            return Result.Failure(AuthenticationErrors.PasswordChange);
        }

        await this.LogoutUserAsync();
        return Result.Success();
    }

    public async Task<Result<string>> RefreshAccessTokenAsync()
    {
        string? refreshToken = _httpContextAccessor.HttpContext.User.FindFirstValue("refreshToken");
        if(refreshToken == null)
        {
            return Result.Failure<string>(AuthenticationErrors.WrongToken);
        }
        
        var user = await _userManager.Users.FirstAsync(u => u.RefreshToken == refreshToken);
        if (user == null)
        {
            return Result.Failure<string>(AuthenticationErrors.UserNotFound);
        }

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;

        var newToken = await _tokenService.GenerateAccessTokenAsync(user);
        return Result.Success(newToken);
    } 
}