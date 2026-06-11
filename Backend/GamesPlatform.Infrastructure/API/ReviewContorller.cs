using Microsoft.AspNetCore.Mvc;

namespace GamesPlatform.Infrastructure.API;

[ApiController]
[Route("[controller]")]
public class ReviewContorller : ControllerBase
{
    [HttpGet("{id}")]
    public async Task GetReviewById(int id)
    {
        
    }
}