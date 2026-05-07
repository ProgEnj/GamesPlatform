namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBInvolvedCompany
{
    public int Id { get; set; }
    
    public IGDBCompany company { get; set; }
    
    public bool IsDeveloper { get; set; }
    public bool IsPublisher { get; set; }
    public bool IsPorting { get; set; }
    public bool IsSupporting { get; set; }
}