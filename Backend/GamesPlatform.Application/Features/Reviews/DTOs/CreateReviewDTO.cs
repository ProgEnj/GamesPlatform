namespace GamesPlatform.Application.Features.Reviews.DTOs;

public class CreateReviewDTO
{
    public string Text { get; set; }
    public int GameId { get; set; }
    public string UserName { get; set; }
}