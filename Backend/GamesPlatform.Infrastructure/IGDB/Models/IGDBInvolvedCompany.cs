namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBInvolvedCompany
{
    public int id { get; set; }
    public int company { get; set; }
    public bool developer { get; set; }
    public bool publisher { get; set; }
    public bool porting { get; set; }
    public bool supporting { get; set; }
}