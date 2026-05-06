using GamesPlatform.Core.Model.Game;

namespace GamesPlatform.UseCases.Features.Game.DTOs;

public class GameDTO
{
    public string Name { get; set; }
    public List<string> AlternativeNames { get; set; }
    public string? VersionTitle { get; set; }
    public int CoverId { get; set; }
    public string Summary { get; set; }
    public List<string> LanguageSupports { get; set; }
    public List<string> Genres { get; set; }
    public List<Company> Companies { get; set; }
   
    public DateTime FirstReleaseDate { get; set; }
}