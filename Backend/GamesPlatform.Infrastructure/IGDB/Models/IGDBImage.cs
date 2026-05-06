using System.Text.Json.Serialization;

namespace GamesPlatform.Infrastructure.IGDB.Models;

public class IGDBImage
{
    public int image_id { get; set; }
    public string url { get; set; }
    
    public bool alpha_channel { get; set; }
    public bool animated { get; set; }
    public Guid checksum { get; set; }
    
    public int height { get; set; }
    public int width { get; set; }
}