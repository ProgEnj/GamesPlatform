namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBPlatform
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public IGDBImage platform_logo { get; set; }
}