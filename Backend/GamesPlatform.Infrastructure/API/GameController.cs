using GamesPlatform.Infrastructure.IGDB;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class GameController(IIGDBService _gameService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById(int id)
    {
        var result = await _gameService.GetGame(id);
        return Ok(result);
    }

}