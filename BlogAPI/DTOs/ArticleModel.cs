namespace BlogAPI.DTOs;

public class ArticleModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime PublishedDate { get; set; }
}