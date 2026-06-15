using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Core.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Profile.Implementation;

public class UserProfileService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager) : IUserProfileService
{
   public async Task<Result<UserProfile>> GetProfileByIdAsync(string id)
   {
      var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
      
      return profile == null ? 
         Result.Failure<UserProfile>(UserProfileErrors.UserProfileNotFound) : profile;

      // if (profile == null) 
      //    return Result.Failure<UserProfile>(UserProfileErrors.UserProfileNotFound);
      //
      // return profile;
   }
   
   public async Task<Result<UserProfile>> GetProfileByUserNameAsync(string userName)
   {
      var user = await _context.Users
         .Include(users => users.UserProfile)
         .FirstOrDefaultAsync(x => x.UserName == userName);
      
      if (user == null) 
         return Result.Failure<UserProfile>(AuthenticationErrors.UserNotFound);

      var profile = user.UserProfile;
      
      return profile == null ? 
         Result.Failure<UserProfile>(UserProfileErrors.UserProfileNotFound) : profile;
   }
   
   public async Task<Result<UserProfile>> CreateNewProfileAsync(string userName)
   {
      //TODO: do something with FavouriteGame field
      var userProfile = new UserProfile(userName, "none");
      
      await _context.UserProfiles.AddAsync(userProfile);
      
      return (await _context.SaveChangesAsync()) != 1 ?
          Result.Failure<UserProfile>(UserProfileErrors.FailedToCreateUserProfile) : userProfile;
   }
}