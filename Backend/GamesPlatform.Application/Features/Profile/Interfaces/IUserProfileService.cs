using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Core.Model.User;

namespace GamesPlatform.Application.Features.Profile.Interfaces;

public interface IUserProfileService
{
    Task<Result<UserProfile>> GetProfileByIdAsync(string id);
    Task<Result<UserProfile>> GetProfileByUserNameAsync(string userName);
    Task<Result<UserProfile>> CreateNewProfileAsync(string userName);
}