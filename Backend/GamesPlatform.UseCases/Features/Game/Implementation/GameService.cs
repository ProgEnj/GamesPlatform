using GamesPlatform.UseCases.Features.Game.Interfaces;

namespace GamesPlatform.UseCases.Features.Game.Implementation;

public class GameService : IGameService
{
   public string GetGame()
   {
      return "works";
   }
}