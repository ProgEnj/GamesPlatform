using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Core.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Profile.Implementation;

public class UserProfileService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager) : IUserProfile
{
   public async Task<UserProfile?> GetUserById(string id)
   {
      return await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
   }
   
   public async Task<UserProfile?> GetUserByUserName(string userName)
   {
      var user = await _userManager.FindByNameAsync(userName);
      if (user == null) return null;

      return await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == user.Id);
   }
   
   public async Task<UserProfile> CreateNewProfile(string userName)
   {
      //TODO: do something with FavouriteGame field
      var userProfile = new UserProfile(userName, "none");
      
      await _context.UserProfiles.AddAsync(userProfile);
      
      if ((await _context.SaveChangesAsync()) != 1)
      {
         throw new Exception($"Failed to create user");
      }

      return userProfile;
   }
}