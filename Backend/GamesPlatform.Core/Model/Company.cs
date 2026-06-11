namespace GamesPlatform.Core.Model;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public bool IsDeveloper { get; set; }
    public bool IsPublisher { get; set; }
    public bool IsPorting { get; set; }
    public bool IsSupporting { get; set; }
    public Uri CompanyLogo { get; set; }
}