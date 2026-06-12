using GamesPlatform.Core.Model.User;

namespace GamesPlatform.Core.Model.Reviews;

public class Review
{
    public string Id { get; set; }
    public string Text { get; set; }
    public int UpvoteCount { get; set; }
    public int DownvoteCount { get; set; }
    public UserProfile Author { get; set; }
    public DomainGame DomainGame { get; set; }
    public List<Comment>? Comments { get; set; }

    protected Review(){}
    
    public Review(string text, UserProfile author, DomainGame domainGame)
    {
        Id = Guid.NewGuid().ToString();
        Text = text;
        Author = author;
        DomainGame = domainGame;
    }
}