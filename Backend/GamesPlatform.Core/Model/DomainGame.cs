namespace GamesPlatform.Core.Model;

public class DomainGame
{
    public string Id { get; set; }
    public int IGDBid { get; set; }
    
    // TODO: IGDBGame to domain model
    // Adding IGDBGame model to domain model requires a lot of effort now
    // So for now I just want to map my core model with api model
    // And if needed later I will expand this to my needs
    
    // public string Name { get; set; }
    // public List<string>? AlternativeNames { get; set; }
    // public string? VersionTitle { get; set; }
    // public string Summary { get; set; }
    // public List<string> LanguageSupports { get; set; }
    // public List<string> Genres { get; set; }
    // public List<Company> Companies { get; set; }
    // public DateTime FirstReleaseDate { get; set; }
    
    // TODO: This is waiting until images implementation
    // public int CoverId { get; set; } 
   
    protected DomainGame() {}
        
    public DomainGame(int igdbId)
    {
        Id = Guid.NewGuid().ToString();
        IGDBid = igdbId;
    }
}