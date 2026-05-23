using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Core.Model.User;

namespace GamesPlatform.Application.Features.Profile.Interfaces;

public interface IUserProfile
{
    Task<UserProfile?> GetUserById(string id);
    Task<UserProfile?> GetUserByUserName(string userName);
    Task CreateNewProfile(ApplicationUser newUser);
}