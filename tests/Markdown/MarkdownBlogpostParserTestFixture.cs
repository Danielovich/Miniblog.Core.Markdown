namespace Miniblog.Core.Tests.Markdown;

public class MarkdownBlogpostParserTestFixture
{
    public string MarkdownContent { get; private set; } = default!;

    public MarkdownBlogpostParserTestFixture()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

        this.MarkdownContent = File.ReadAllText(markdownFile);
    }
}
