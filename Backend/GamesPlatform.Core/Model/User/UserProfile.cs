namespace GamesPlatform.Core.Model.User;

public class UserProfile
{
    public string Id { get; set; } 
    public string ProfileName { get; set; } 
    public string FavouriteGame { get; set; }
    // TODO: Profile picture
    
    public UserProfile(string id, string profileName, string favouriteGame)
    {
        Id = id;
        ProfileName = profileName;
        FavouriteGame = favouriteGame;
    }
}
