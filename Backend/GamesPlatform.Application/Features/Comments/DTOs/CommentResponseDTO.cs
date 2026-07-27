using GamesPlatform.Core.Model.Reviews;

namespace GamesPlatform.Application.Features.Comments.DTOs;

public class CommentResponseDTO
{
    public string Id { get; set; }
    public string Text { get; set; }
    public int UpvoteCount { get; set; }
    public int DownvoteCount { get; set; }
    public List<Comment> Replies { get; set; }

    public CommentResponseDTO(string id, string text, int upvoteCount, int downvoteCount, List<Comment> replies)
    {
        Id = id;
        Text = text;
        UpvoteCount = upvoteCount;
        DownvoteCount = downvoteCount;
        Replies = replies;
    }
}