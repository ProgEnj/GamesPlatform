using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Application.Features.Reviews.DTOs;
using GamesPlatform.Application.Features.Reviews.Interfaces;
using GamesPlatform.Infrastructure.IGDB;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class GameController(IIGDBService _igdbGameService, IGameService _gameService, 
    IReviewService _reviewService) : ControllerBase
{
    //TODO: make uri not by id but by game name
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById(int id)
    {
        var result = await _igdbGameService.GetGameAsync(id);
        
        // This will be here for some time cause
        // Cause it's easier to put it in this 'gateway' place
        // than to add in every post, need to remove later of course
        await _gameService.CreateGameAsync(id);
        return Ok(result);
    }

}