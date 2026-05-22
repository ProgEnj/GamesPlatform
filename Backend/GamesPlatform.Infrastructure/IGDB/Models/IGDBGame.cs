namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBGame
{
   public int id { get; set; }
   public string name { get; set; }
   public List<IGDBAlternativeName> alternative_names { get; set; }
   public string? version_title { get; set; }
   public IGDBImage cover { get; set; }
   public string summary { get; set; }
   public List<IGDBLanguageSupport> language_supports { get; set; }
   public List<IGDBPlatform> platforms { get; set; }
   public List<IGDBGenre> genres { get; set; }
   public List<IGDBImage> screenshots { get; set; }
   public List<IGDBInvolvedCompany> involved_companies { get; set; }
   
   // TODO: this is Unix Time Stamp, check if need something special for that
   public double first_release_date { get; set; }
}