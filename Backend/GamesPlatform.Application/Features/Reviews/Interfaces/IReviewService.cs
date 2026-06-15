using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Core.Model.Reviews;

namespace GamesPlatform.Application.Features.Reviews.Interfaces;

public interface IReviewService
{
    public Task<Result> CreateReviewAsync(CreateReviewDTO review);
    public Task<Result<List<ReviewResponseDTO>>> GetAllGameReviewsAsync(int gameId);
}