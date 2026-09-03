using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Comments.DTOs;
using GamesPlatform.Application.Features.Comments.Interfaces;
using GamesPlatform.Application.Features.Profile.Interfaces;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Core.Model.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Comments.Implementations;

public class CommentsService(ApplicationDbContext _context, IReviewService _reviewService, IUserProfileService _userProfileService) : ICommentsService
{
    public async Task<Result<List<CommentResponseDTO>>> GetReviewCommentsAsync(string reviewId, int skip, int top)
    {
        // Find a way to manage self one to many relationship
        var comments = await _context.Comments
            .Where(x => x.Review.Id == reviewId && x.ReplyTo == null)
            .Skip(skip)
            .Take(top)
            .Select(comment => new CommentResponseDTO(comment.Id, comment.Text,
                comment.UpvoteCount, comment.DownvoteCount, comment.Comments))
            .ToListAsync();
        
        if (comments.Count() == 0)
        {
            return new Result<List<CommentResponseDTO>>(new(), false, CommentsErrors.NoCommetsForReview);
        }

        return comments;
    }

    // TODO: Trying to solve another problem now,
    //  Comments can only be created by autheticated users,
    //  so better practice will be to extract identity from JWT token
    public async Task<Result> CreateCommentForReviewAsync(string reviewId, CreateCommentRequestDTO createDTO)
    {
        var review = await _reviewService.GetDomainReviewByIdAsync(reviewId);
        var userProfile = await _userProfileService.GetProfileByUserNameAsync(createDTO.AuthorName);

        if (!review.IsSuccess || !userProfile.IsSuccess)
        {
            return Result.Failure(CommentsErrors.InvalidUserOrReview);
        }
        
        _context.Comments.Add(new Comment(createDTO.Text, userProfile.Value, review.Value));
        
        if ((await _context.SaveChangesAsync()) != 1)
        {
            return Result.Failure(CommentsErrors.FailedToCreateComment);
        }

        return Result.Success();
    }

    public async Task<Result> CreateReplyToCommentAsync(string reviewId, CreateReplyRequestDTO createDTO)
    {
        var review = await _reviewService.GetDomainReviewByIdAsync(reviewId);
        var userProfile = await _userProfileService.GetProfileByUserNameAsync(createDTO.AuthorName);
        var replyToComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == createDTO.ReplyTo);

        if (!review.IsSuccess || !userProfile.IsSuccess || replyToComment == null)
        {
            return Result.Failure(CommentsErrors.InvalidUserOrReview);
        }
        
        replyToComment.Comments.Add(new Comment(createDTO.Text, userProfile.Value, review.Value, replyToComment));
        
        if ((await _context.SaveChangesAsync()) != 1)
        {
            return Result.Failure(CommentsErrors.FailedToCreateReply);
        }

        return Result.Success();
    }
}