using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Core.Model;

namespace GamesPlatform.Application.Features.Game.Interfaces;

public interface IGameService
{
    public Task<Result> CreateGameAsync(int igdbId);
    public Task<Result<DomainGame>> GetGameByIdAsync(string gameId);
}