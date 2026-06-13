using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class ReviewContorller(IGameService _gameService, IReviewService _reviewService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task GetReviewById(int id)
    {
        
    }
    
    [HttpPost("{id}/review")]
    public async Task<IActionResult> PostReview([FromRoute] int id, CreateReviewRequestDTO review)
    {
        
        if (!(await _gameService.CreateGameAsync(id)).IsSuccess)
        {
            return StatusCode(500);
        }

        var result = await _reviewService.CreateReviewAsync(id, review);

        return result.IsSuccess ? Ok(200) :  StatusCode(500, result.Error.Message);
    }
    
    //TODO: make uri not by id but by game name
    //TODO: pagination
    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetAllGameReivews(int id)
    {
        var reviews = await _reviewService.GetAllGameReviewsAsync(id);

        return reviews.IsSuccess ? Ok(reviews) : Ok(reviews.Error.Message);
    }
}