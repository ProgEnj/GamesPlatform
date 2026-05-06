using System.Diagnostics.CodeAnalysis;
using GamesPlatform.Infrastructure.IGDB;
using GamesPlatform.UseCases.Features.Game.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class GameController(IIGDBService _gameService) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetGameById()
    {
        var result = await _gameService.GetGenre();
        return Ok(result);
    }

}