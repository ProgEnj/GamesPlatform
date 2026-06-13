using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Game.Implementation;

public class GameService(ApplicationDbContext _context) : IGameService
{
   public async Task<Result> CreateGameAsync(int igdbId)
   {
      if (await _context.Games.FirstOrDefaultAsync(x => x.IGDBid == igdbId) == null)
      {
         await _context.AddAsync(new DomainGame(igdbId));
         if ((await _context.SaveChangesAsync()) != 1)
         {
            return Result.Failure(GameErrors.FailedToCreateGame);
         }
      }

      return Result.Success();
   }

   public async Task<Result<DomainGame>> GetGameByIgdbIdAsync(int igdbId)
   {
      var game = await _context.Games.FirstOrDefaultAsync(x => x.IGDBid == igdbId);

      return game == null ? 
         Result.Failure<DomainGame>(GameErrors.GameNotFound) : game;
   }
   
   public async Task<Result<DomainGame>> GetGameByDomainIdAsync(string domainGameId)
   {
      var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == domainGameId);

      return game == null ? 
         Result.Failure<DomainGame>(GameErrors.GameNotFound) : game;
   }
}