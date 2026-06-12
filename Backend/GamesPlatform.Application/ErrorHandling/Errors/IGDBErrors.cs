namespace GamesPlatform.Application.ErrorHandling.Errors;

public class IGDBErrors
{
    public static readonly Error GameNotFound = new("Requested game doesn't exist");
}
