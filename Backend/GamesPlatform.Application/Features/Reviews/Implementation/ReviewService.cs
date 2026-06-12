using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Core.Model.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Reviews.Implementation;

public class ReviewService(ApplicationDbContext _context, 
    IUserProfileService _userProfileService, IGameService _gameService) : IReviewService
{
    public async Task<Result> CreateReviewAsync(int gameId, CreateReviewRequestDTO review)
    {
        var userProifle = await _userProfileService.GetProfileByIdAsync(review.UserId);
        var game = await _gameService.GetGameByIdAsync(review.GameId);
        
        _context.Add(new Review(review.Text, userProifle.Value, game.Value));
        if ((await _context.SaveChangesAsync()) != 1)
        {
            return Result.Failure(ReviewErrors.ReviewNotFound);
        }

        return Result.Success();
    }

    public async Task<Result<List<Review>>> GetAllGameReviewsAsync(int gameId)
    {
        var reviews = await _context.Reviews.Where(x => x.DomainGame.IGDBid == gameId).ToListAsync();
        
        if (reviews.Count() == 0)
            return new Result<List<Review>>(new(), false, GameErrors.GameNotFound);

        return reviews;
    }
}