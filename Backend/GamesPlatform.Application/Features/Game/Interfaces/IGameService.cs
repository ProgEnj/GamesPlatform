using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.Features.Game.DTOs;
using GamesPlatform.Core.Model;

namespace GamesPlatform.Application.Features.Game.Interfaces;

public interface IGameService
{
    public Task<Result> CreateGameAsync(CreateDomainGameDTO gameDto);
    public Task<Result<DomainGame>> GetGameByIgdbIdAsync(int igdbId);
    public Task<Result<DomainGame>> GetGameByDomainIdAsync(string domainGameId);
    public Task<Result<DomainGame>> GetGameByUriName(string uriName);
}