using GamesPlatform.Application.ErrorHandling;
using GamesPlatform.Application.ErrorHandling.Errors;
using GamesPlatform.Application.Features.Game.DTOs;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Persistance;
using GamesPlatform.Core.Helpers;
using GamesPlatform.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Features.Game.Implementation;

public class GameService(ApplicationDbContext _context) : IGameService
{
   public async Task<Result> CreateGameAsync(CreateDomainGameDTO gameDto)
   {
      if (await _context.Games.FirstOrDefaultAsync(x => x.IGDBid == gameDto.IGDBId) == null)
      {
         await _context.AddAsync(new DomainGame(gameDto.IGDBId, KebabCaseTransform.ToKebabCase(gameDto.Name)));
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
   
   public async Task<Result<DomainGame>> GetGameByUriName(string uriName)
   {
      var game = await _context.Games.FirstOrDefaultAsync(x => x.UriName == uriName);

      return game == null ? 
         Result.Failure<DomainGame>(GameErrors.GameNotFound) : game;
   }
}