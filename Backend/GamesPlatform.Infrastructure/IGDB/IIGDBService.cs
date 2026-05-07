using GamesPlatform.Infrastructure.IGDB.Models;

namespace GamesPlatform.Infrastructure.IGDB;

public interface IIGDBService
{
    Task<IGDBGenre> GetGenre(int id);
    Task<IGDBGame> GetGame(int id);
}