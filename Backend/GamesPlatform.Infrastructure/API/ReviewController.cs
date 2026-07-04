using System.Security.Claims;
using GamesPlatform.Application.Features.Comments.DTOs;
using GamesPlatform.Application.Features.Comments.Interfaces;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class ReviewsController(IReviewService _reviewService, ICommentsService _commentsService) : ControllerBase
{
    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(string reviewId)
    {
        var result = await _reviewService.GetReviewByIdAsync(reviewId);
        
        return result.IsSuccess ? Ok(result.Value) : Ok(result.Error.Message);
    }
    
    [HttpPost("{gameId}/review")]
    [Authorize]
    public async Task<IActionResult> PostReview([FromRoute] int gameId, [FromBody] string text)
    {
        // TODO: Maybe make some helper to extract claims from identity
        //  Also this looks kida hacky, maybe there is better solution for this
        var userNameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        var reviewDTO = new CreateReviewDTO() { GameId = gameId, Text = text, UserName = userNameClaim.Value };
        
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
    
    [HttpGet("{reviewId}/comments")]
    public async Task<IActionResult> GetReviewComments([FromRoute] string reviewId, [FromQuery] int skip, [FromQuery] int top)
    {
        var result = await _commentsService.GetReviewCommentsAsync(reviewId, skip, top);
        return result.IsSuccess ? Ok(result.Value) : Ok(result.Error.Message);
    }

    [HttpPost("{reviewId}/comment")]
    [Authorize]
    public async Task<IActionResult> CreateCommentToReview([FromRoute] string reviewId, CreateCommentRequestDTO createDto)
    {
        var userNameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        createDto.AuthorName = userNameClaim.Value;
        var result = await _commentsService.CreateCommentForReviewAsync(reviewId, createDto);
        
        return result.IsSuccess ? Created() : StatusCode(500, result.Error.Message);
    }
    
    [HttpPost("{reviewId}/{commentId}")]
    [Authorize]
    public async Task<IActionResult> CreateReplyToComment([FromRoute] string reviewId, [FromRoute] string commentId, 
        CreateCommentRequestDTO createDto)
    {
        var userNameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        createDto.AuthorName = userNameClaim.Value;
        var result = await _commentsService.CreateCommentForReviewAsync(reviewId, createDto);
        
        return result.IsSuccess ? Created() : StatusCode(500, result.Error.Message);
    }
}