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
    public async Task<Result<ReviewResponseDTO>> GetReviewByIdAsync(string id)
    {
        var result = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            return Result.Failure<ReviewResponseDTO>(ReviewErrors.ReviewNotFound);
        }

        return new ReviewResponseDTO(result.Id, result.Text, result.UpvoteCount,
            result.Author.Id, result.Author.ProfileName, result.DownvoteCount);
    }
    
    public async Task<Result<Review>> GetDomainReviewByIdAsync(string id)
    {
        var result = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            return Result.Failure<Review>(ReviewErrors.ReviewNotFound);
        }

        return result;
    }
    
    public async Task<Result<ReviewResponseDTO>> GetDomainByIdAsync(string id)
    {
        var result = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            return Result.Failure<ReviewResponseDTO>(ReviewErrors.ReviewNotFound);
        }

        return new ReviewResponseDTO(result.Id, result.Text, result.UpvoteCount,
            result.Author.Id, result.Author.ProfileName, result.DownvoteCount);
    }
    
    public async Task<Result> CreateReviewAsync(CreateReviewDTO review)
    {
        var userProifle = await _userProfileService.GetProfileByUserNameAsync(review.UserName);
        var game = await _gameService.GetGameByIgdbIdAsync(review.GameId);
        
        _context.Add(new Review(review.Text, userProifle.Value, game.Value));
        if ((await _context.SaveChangesAsync()) != 1)
        {
            return Result.Failure(ReviewErrors.FailedToCreateReview);
        }

        return Result.Success();
    }

    public async Task<Result<List<ReviewResponseDTO>>> GetAllGameReviewsAsync(int gameId)
    {
        await _gameService.GetGameByIgdbIdAsync(gameId);
        var reviews = await _context.Reviews
            .Include(review => review.Author)
            .Where(x => x.DomainGame.IGDBid == gameId)
            .Select(x => new ReviewResponseDTO(x.Id, x.Text, x.UpvoteCount, x.Author.Id, 
                x.Author.ProfileName, x.DownvoteCount))
            .ToListAsync();

        if (reviews.Count() == 0)
        {
            return new Result<List<ReviewResponseDTO>>(new(), false, ReviewErrors.NoReviewsForTheGame);
        }

        return reviews;
    }
}