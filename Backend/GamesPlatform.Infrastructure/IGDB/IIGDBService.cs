using GamesPlatform.Infrastructure.ErrorHandling;
using GamesPlatform.Infrastructure.IGDB.Models;

namespace GamesPlatform.Infrastructure.IGDB;

public interface IIGDBService
{
    Task<Result<IGDBGenre>> GetGenre(int id);
    Task<Result<IGDBGame>> GetGame(int id);
}