namespace BlogAPI.Models;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime LastEdited { get; set; }
    public DateTime Published { get; set; }
    
}