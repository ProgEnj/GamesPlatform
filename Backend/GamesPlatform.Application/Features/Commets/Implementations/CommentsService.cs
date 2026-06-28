using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Commets.DTOs;
using GamesPlatform.Application.Features.Commets.Interfaces;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Core.Model.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Commets;

public class CommentsService(ApplicationDbContext _context, IReviewService _reviewService, IUserProfileService _userProfileService) : ICommentsService
{
    public async Task<Result<List<CommentResponseDTO>>> GetReviewCommentsAsync(string reviewId, int skip, int top)
    {
        var comments = await _context.Comments
            .Where(x => x.Review.Id == reviewId)
            .Skip(skip)
            .Take(top)
            .Select(comment => new CommentResponseDTO(comment.Id, comment.Text,
                comment.UpvoteCount, comment.DownvoteCount))
            .ToListAsync();
        
        if (comments.Count() == 0)
        {
            return new Result<List<CommentResponseDTO>>(new(), false, CommentsErrors.NoCommetsForReview);
        }

        return comments;
    }

    public async Task<Result> CreateCommentForReviewAsync(string reviewId, CreateCommentRequestDTO createDTO)
    {
        var review = await _reviewService.GetDomainReviewByIdAsync(reviewId);
        var userProfile = await _userProfileService.GetProfileByUserNameAsync(createDTO.AuthorName);

        if (!review.IsSuccess || !userProfile.IsSuccess)
        {
            return Result.Failure(CommentsErrors.InvalidUserOrReview);
        }
        
        _context.Comments.Add(new Comment(createDTO.Text, userProfile.Value, review.Value));
        await _context.SaveChangesAsync();
        
        if ((await _context.SaveChangesAsync()) != 1)
        {
            return Result.Failure(CommentsErrors.FailedToCreateComment);
        }

        return Result.Success();
    }
}