using System.Security.Claims;
using GamesPlatform.Application.Features.Comments.DTOs;
using GamesPlatform.Application.Features.Comments.Interfaces;
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
    
    [HttpPost("{reviewId}/{commentId}/comment")]
    [Authorize]
    public async Task<IActionResult> CreateReplyToComment([FromRoute] string reviewId, [FromRoute] string commentId, 
        CreateReplyRequestDTO createDto)
    {
        var userNameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        createDto.AuthorName = userNameClaim.Value;
        createDto.ReplyTo = commentId;
        
        var result = await _commentsService.CreateReplyToCommentAsync(reviewId, createDto);
        
        return result.IsSuccess ? Created() : StatusCode(500, result.Error.Message);
    }
}