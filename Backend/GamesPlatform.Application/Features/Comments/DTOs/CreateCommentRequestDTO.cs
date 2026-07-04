namespace GamesPlatform.Application.Features.Comments.DTOs;

public class CreateCommentRequestDTO
{
    public string Text { get; set; }
    public string? AuthorName { get; set; }
}