using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Infrastructure.IGDB.Models;

namespace GamesPlatform.Infrastructure.IGDB;

public interface IIGDBService
{
    Task<Result<IGDBGenre>> GetGenreAsync(int id);
    Task<Result<IGDBGame>> GetGameByIdAsync(int id);
}