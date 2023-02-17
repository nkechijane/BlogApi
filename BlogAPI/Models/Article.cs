namespace BlogAPI.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime LastEdited { get; set; }
    public DateTime Published { get; set; }
    
}