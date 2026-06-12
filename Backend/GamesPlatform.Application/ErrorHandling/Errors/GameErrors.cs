namespace GamesPlatform.Application.ErrorHandling.Errors;

public static class GameErrors
{
    public static readonly Error FailedToCreateGame = new("Failed to create DomainGame");
    public static readonly Error GameNotFound = new("Requested game not found");
}