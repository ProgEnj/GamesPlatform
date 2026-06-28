namespace GamesPlatform.Application.Features.Commets.DTOs;

public class CreateCommentRequestDTO
{
    public string Text { get; set; }
    public string ReviewId { get; set; }
    public string AuthorName { get; set; }
}