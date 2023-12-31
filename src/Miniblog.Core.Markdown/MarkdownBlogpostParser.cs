namespace Miniblog.Core.Markdown;

public class MarkdownBlogpostParser
{
    private const string validMarkdownComment1 = "[//]: #";
    private const string validMarkdownComment2 = "[//]:#";

    public string Markdown { get; set; } = default!;
    public MarkdownPost MarkdownPost { get; private set; }

    public MarkdownBlogpostParser(string markdown)
    {
        ArgumentNullException.ThrowIfNull(markdown);

        this.Markdown = markdown;
        this.MarkdownPost = new MarkdownPost();
    }

    /// <summary>
    /// Parses comments as blog post properties
    /// If successful you can use Post from here on after.
    /// </summary>
    public async Task ParseCommentsAsPropertiesAsync()
    {
        using var reader = new StringReader(this.Markdown);

        // line of content from the Markdown
        string? line;

        // Convention is that all comments from top of the MarkDown is a future
        // post setting. As soon as the parser sees something else it breaks.
        while ((line = reader.ReadLine()) != null)
        {
            if (!(line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2)))
            {
                break;
            }

            if (line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                var markdownComment = new MarkdownComment(line);

                // somewhat a tedious bit of code but it's easy to understand
                // we start by looking at what type of comment we have on our hands
                // from there we extract the comment value 
                switch (markdownComment.GetValueBetweenFirstQuoteAndColon())
                {
                    case "title":
                        MarkdownPost.Title = markdownComment.AsPostProperty("title").Value;
                        break;
                    case "slug":
                        MarkdownPost.Slug = markdownComment.AsPostProperty("slug").Value;
                        break;
                    case "pubDate":
                        MarkdownPost.PubDate = markdownComment.AsPostProperty("pubDate").ParseToDate();
                        break;
                    case "lastModified":
                        MarkdownPost.LastModified = markdownComment.AsPostProperty("lastModified").ParseToDate();
                        break;
                    case "excerpt":
                        MarkdownPost.Excerpt = markdownComment.AsPostProperty("excerpt").Value;
                        break;
                    case "categories":
                        markdownComment.ToProperty("categories", ',').Select(n => n.Value).ToList().ForEach(MarkdownPost.Categories.Add);
                        break;
                    case "isPublished":
                        MarkdownPost.IsPublished = markdownComment.AsPostProperty("isPublished").ParseToBool();
                        break;
                    default:
                        break;
                } 
            }
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Takes all content which is not markdown comments property
    /// </summary>
    public async Task ParseContent()
    {
        using var reader = new StringReader(this.Markdown);
        using var contenWriter = new StringWriter();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                continue;
            }

            if (!line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
            {
                contenWriter.WriteLine(line);
            }

            
        }

        MarkdownPost.Content = contenWriter.ToString();

        await Task.CompletedTask;
    }
}
