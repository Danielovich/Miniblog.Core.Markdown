namespace Miniblog.Core.Markdown;

public class MarkdownPost
{
    public IList<string> Categories { get; } = new List<string>();

    public IList<string> Tags { get; } = new List<string>();

    public string Content { get; set; } = string.Empty;

    public string Excerpt { get; set; } = string.Empty;

    public bool IsPublished { get; set; } = true;

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public DateTime PubDate { get; set; } = DateTime.UtcNow;

    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
