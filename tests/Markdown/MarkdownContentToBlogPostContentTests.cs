namespace Miniblog.Core.Tests.Markdown;
public class MarkdownContentToBlogPostContentTests : IClassFixture<MarkdownBlogpostParserTestFixture>
{
    private readonly MarkdownBlogpostParserTestFixture markdownBlogpostParserTestFixture;

    public MarkdownContentToBlogPostContentTests(MarkdownBlogpostParserTestFixture markdownBlogpostParserTestFixture)
    {
        this.markdownBlogpostParserTestFixture = markdownBlogpostParserTestFixture;
    }
    [Fact]
    public async Task Parse_As_Content()
    {
        var markDownBlogpostParser = new MarkdownBlogpostParser(this.markdownBlogpostParserTestFixture.MarkdownContent);
        await markDownBlogpostParser.ParseContent();

        Assert.True(markDownBlogpostParser.MarkdownPost.Content != string.Empty);

    }
}
