using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Core.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Profile.Implementation;

public class UserProfileServiceService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager) : IUserProfileService
{
   public async Task<Result<UserProfile>> GetProfileByIdAsync(string id)
   {
      var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
      
      return profile == null ? 
         Result.Failure<UserProfile>(new Error("User not found")) : profile;

      // if (profile == null) 
      //    return Result.Failure<UserProfile>(new Error("User not found"));
      //
      // return profile;
   }
   
   public async Task<Result<UserProfile>> GetProfileByUserNameAsync(string userName)
   {
      var user = await _userManager.FindByNameAsync(userName);
      
      if (user == null) 
         return Result.Failure<UserProfile>(new Error("User not found"));

      var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == user.Id);
      
      return profile == null ? 
         Result.Failure<UserProfile>(new Error("User not found")) : profile;
   }
   
   public async Task<Result<UserProfile>> CreateNewProfileAsync(string userName)
   {
      //TODO: do something with FavouriteGame field
      var userProfile = new UserProfile(userName, "none");
      
      await _context.UserProfiles.AddAsync(userProfile);
      
      return (await _context.SaveChangesAsync()) != 1 ?
          Result.Failure<UserProfile>(new Error("Failed to create user")) : userProfile;
   }
}