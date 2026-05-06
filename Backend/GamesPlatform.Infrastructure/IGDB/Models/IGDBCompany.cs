namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBCompany
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public IGDBImage logo { get; set; }
}