using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.Features.Commets.DTOs;

namespace GamesPlatform.Application.Features.Commets.Interfaces;

public interface ICommentsService
{
    public Task<Result<List<CommentResponseDTO>>> GetReviewCommentsAsync(string reviewId, int skip, int top);
    public Task<Result> CreateCommentForReviewAsync(string reviewId, CreateCommentRequestDTO createDTO);
}