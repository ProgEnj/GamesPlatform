namespace GamesPlatform.Application.ErrorHandling.Errors;

public static class CommentsErrors
{
    public static readonly Error NoCommetsForReview = new("No comments for the requested review");
    public static readonly Error FailedToCreateComment = new("Failed to create comment");
    public static readonly Error FailedToCreateReply = new("Failed to create reply");
    public static readonly Error InvalidUserOrReview = new("User or proifle not found");
}