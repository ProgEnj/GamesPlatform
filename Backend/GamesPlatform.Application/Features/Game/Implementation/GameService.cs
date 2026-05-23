using GamesPlatform.Application.Features.Game.Interfaces;

namespace GamesPlatform.Application.Features.Game.Implementation;

public class GameService : IGameService
{
   public string GetGame()
   {
      return "works";
   }
}