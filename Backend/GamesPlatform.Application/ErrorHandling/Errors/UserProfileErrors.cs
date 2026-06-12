namespace GamesPlatform.Application.ErrorHandling.Errors;

public class UserProfileErrors
{
    public static readonly Error UserProfileNotFound = new("Requested user profile not found");
    public static readonly Error FailedToCreateUserProfile = new("Failed to create user profile");
}