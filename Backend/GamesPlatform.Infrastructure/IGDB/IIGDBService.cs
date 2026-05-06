using GamesPlatform.Infrastructure.IGDB.DTOs;
using GamesPlatform.Infrastructure.IGDB.Models;

namespace GamesPlatform.Infrastructure.IGDB;

public interface IIGDBService
{
    Task<IGDBGenre> GetGenre();
}