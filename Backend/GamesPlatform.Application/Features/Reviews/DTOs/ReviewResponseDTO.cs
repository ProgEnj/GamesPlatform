using GamesPlatform.Core.Model.Reviews;

namespace GamesPlatform.Application.Features.Reviews.DTOs;

public class ReviewResponseDTO
{
    
    public string Id { get; set; }
    public string Text { get; set; }
    public int UpvoteCount { get; set; }
    public string authorId { get; set; }
    public string AutorName { get; set; }
    public int DownvoteCount { get; set; }

    public ReviewResponseDTO(string id, string text, int upvoteCount, string authorId, string autorName, int downvoteCount)
    {
        Id = id;
        Text = text;
        UpvoteCount = upvoteCount;
        this.authorId = authorId;
        AutorName = autorName;
        DownvoteCount = downvoteCount;
    }
}