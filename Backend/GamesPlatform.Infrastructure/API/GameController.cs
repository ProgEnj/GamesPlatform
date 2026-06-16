using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Infrastructure.IGDB;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class GameController(IIGDBService _igdbGameService, IGameService _gameService) : ControllerBase
{
    //TODO: make uri not by id but by game name
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameById(int id)
    {
        var result = await _igdbGameService.GetGameByIdAsync(id);
        
        // This will be here for some time cause
        // Cause it's easier to put it in this 'gateway' place
        // than to add in every post, need to remove later of course
        
        // TODO:
        // 1. Helper in core that transforms game name from kebab case and
        //    compares it to name from igdb, we still return igdbId to front
        //    Example "The Witcher 3: Wild Hunt" -> "the-witcher-3-wild-hunt"
        // 2. New method in IGDB service that returns game by name.
        //    Method will use helper funciton 
        // 3. CreateDomainGameDTO with igdbId and primaryName that we get from
        //    IGDBService, to save game with name in db
        
        
        await _gameService.CreateGameAsync(id);
        return Ok(result);
    }
}