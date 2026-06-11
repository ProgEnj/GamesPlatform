namespace GamesPlatform.Application.Features.Reviews.DTOs;

public class CreateReviewRequestDTO
{
    public string Text { get; set; }
    public string UserId { get; set; }
    public string GameId { get; set; }
}