namespace Miniblog.Core.Markdown.Tests;

public class MarkdownBlogpostParserTestFixture
{
    public string MarkdownContent { get; private set; } = default!;

    public MarkdownBlogpostParserTestFixture()
    {
        var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

        MarkdownContent = File.ReadAllText(markdownFile);
    }
}
