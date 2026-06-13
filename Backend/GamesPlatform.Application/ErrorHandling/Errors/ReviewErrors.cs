namespace GamesPlatform.Application.ErrorHandling.Errors;

public class ReviewErrors
{
    public static readonly Error ReviewNotFound = new("Requested review not found");
    public static readonly Error NoReviewsForTheGame = new("There are no reviews for the requested game");
}