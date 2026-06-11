using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.Features.Reviews.DTOs;

namespace GamesPlatform.Application.Features.Reviews.Interfaces;

public interface IReviewService
{
    public Task<Result> CreateReviewAsync(int gameId, CreateReviewRequestDTO review);
}