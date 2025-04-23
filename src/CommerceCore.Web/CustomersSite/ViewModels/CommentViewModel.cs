namespace CustomersSite.ViewModels;

public class CommentViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public int rating { get; set; }
}
