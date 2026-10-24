using GamesPlatform.Core.Model.Reviews;

namespace GamesPlatform.Application.Features.Comments.DTOs;

public class CommentResponseDTO
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public int UpvoteCount { get; set; }
    public int DownvoteCount { get; set; }
    public List<CommentResponseDTO>? Replies { get; set; }

    public CommentResponseDTO(string id, string text, string author, int upvoteCount, 
        int downvoteCount, List<CommentResponseDTO> replies)
    {
        Id = id;
        Text = text;
        Author = author;
        UpvoteCount = upvoteCount;
        DownvoteCount = downvoteCount;
        Replies = replies;
    }
}