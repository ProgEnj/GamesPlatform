namespace GamesPlatform.Application.Features.Commets.DTOs;

public class CommentResponseDTO
{
    public string Id { get; set; }
    public string Text { get; set; }
    public int UpvoteCount { get; set; }
    public int DownvoteCount { get; set; }

    public CommentResponseDTO(string id, string text, int upvoteCount, int downvoteCount)
    {
        Id = id;
        Text = text;
        UpvoteCount = upvoteCount;
        DownvoteCount = downvoteCount;
    }
}