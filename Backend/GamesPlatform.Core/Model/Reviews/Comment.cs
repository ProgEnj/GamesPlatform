using GamesPlatform.Core.Model.User;

namespace GamesPlatform.Core.Model.Reviews;

public class Comment
{
    public string Id { get; set; }
    public string Text { get; set; }
    public int UpvoteCount { get; set; }
    public int DownvoteCount { get; set; }
    public UserProfile Author { get; set; }
    public Review Review { get; set; }
    public List<Comment>? Comments { get; set; }

    public Comment(string text, UserProfile author, Review review)
    {
        Id = Guid.NewGuid().ToString();
        Text = text;
        Author = author;
        Review = review;
    }
}