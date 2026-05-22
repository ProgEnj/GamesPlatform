namespace GamesPlatform.Infrastructure.ErrorHandling.Errors;

public class IGDBErrors
{
    public static readonly Error GameNotFound = new("Requested game does'nt exist");
}
