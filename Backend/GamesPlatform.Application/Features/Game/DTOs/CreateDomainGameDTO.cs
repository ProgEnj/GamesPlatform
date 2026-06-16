namespace GamesPlatform.Application.Features.Game.DTOs;

public class CreateDomainGameDTO
{
    public int IGDBId { get; set; }
    public string Name { get; set; }

    public CreateDomainGameDTO(int igdbId, string name)
    {
        IGDBId = igdbId;
        this.Name = name;
    }
}