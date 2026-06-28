using System.Security.Claims;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class ReviewsController(IReviewService _reviewService) : ControllerBase
{
    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(string id)
    {
        var result = await _reviewService.GetReviewByIdAsync(id);
        
        return result.IsSuccess ? Ok(result.Value) : Ok(result.Error.Message);
    }
    
    [HttpPost("{gameId}/review")]
    [Authorize]
    public async Task<IActionResult> PostReview([FromRoute] int gameId, [FromBody] string text)
    {
        // TODO: Maybe make some helper to extract claims from identity
        var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        var reviewDTO = new CreateReviewDTO() { GameId = gameId, Text = text, UserName = userName.Value };
        
        var result = await _reviewService.CreateReviewAsync(reviewDTO);
        return result.IsSuccess ? Created() :  StatusCode(500, result.Error.Message);
    }
    
    //TODO: pagination
    [HttpGet("{gameId}/reviews")]
    public async Task<IActionResult> GetAllGameReivews(int gameId)
    {
        var reviews = await _reviewService.GetAllGameReviewsAsync(gameId);

        return reviews.IsSuccess ? Ok(reviews.Value) : Ok(reviews.Error.Message);
    }
}