using GamesPlatform.Application.Features.Game.DTOs;
using GamesPlatform.Application.Features.Game.Interfaces;
using GamesPlatform.Infrastructure.IGDB;
using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class GamesController(IIGDBService _igdbGameService, IGameService _gameService) : ControllerBase
{
    //TODO: make uri not by id but by game name
    [HttpGet("id")]
    public async Task<IActionResult> GetGameById([FromQuery] int gameId)
    {
        var result = await _igdbGameService.GetGameByIdAsync(gameId);
        
        // This will be here for some time
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
        
        
        await _gameService.CreateGameAsync(new CreateDomainGameDTO(result.Value.id, result.Value.name));
        return Ok(result);
    }

    [HttpGet("{uriName}")]
    public async Task<IActionResult> GetGameByUriName(string uriName)
    {
        var domainGame = await _gameService.GetGameByUriName(uriName);
        var result = await _igdbGameService.GetGameByIdAsync(domainGame.Value.IGDBid);
        return Ok(result.Value);
    }
}